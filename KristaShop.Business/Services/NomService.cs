using AutoMapper;
using KristaShop.Business.DTOs;
using KristaShop.Business.Interfaces;
using KristaShop.Common.Models;
using KristaShop.DataAccess.Entities;
using KristaShop.DataAccess.Interfaces;
using KristaShop.DataReadOnly.DTOs;
using KristaShop.DataReadOnly.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KristaShop.Business.Services
{
    public class NomService : INomService
    {
        private readonly INomRepository<Nomenclature> _nomRepo;
        private readonly INomRepository<NomCatalog> _nomCatalogRepo;
        private readonly INomRepository<NomCategory> _nomCategoryRepo;
        private readonly INomRepository<VisibleNomUser> _nomUserRepo;
        private readonly INomRepository<NotVisibleProdCtlg> _visProdCtlgRepo;
        private readonly INomRepository<NotVisibleProdCtgr> _visProdCtgrRepo;
        private readonly INomRepository<NomProdPrice> _prodPriceRepo;
        private readonly INomRepository<NomPhoto> _nomPhotoRepo;

        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly ICatalogItemReadService _nomReadService;

        public NomService
            (INomRepository<Nomenclature> nomRepo, INomRepository<NomCatalog> nomCatalogRepo,
            INomRepository<NomCategory> nomCategoryRepo, INomRepository<VisibleNomUser> nomUserRepo,
            INomRepository<NotVisibleProdCtlg> visProdCtlgRepo, INomRepository<NotVisibleProdCtgr> visProdCtgrRepo,
            INomRepository<NomProdPrice> prodPriceRepo, INomRepository<NomPhoto> nomPhotoRepo,
            IMapper mapper, ICatalogItemReadService nomReadService, IUserService userService)
        {
            _nomRepo = nomRepo;
            _nomCatalogRepo = nomCatalogRepo;
            _nomCategoryRepo = nomCategoryRepo;
            _nomUserRepo = nomUserRepo;
            _visProdCtlgRepo = visProdCtlgRepo;
            _visProdCtgrRepo = visProdCtgrRepo;
            _prodPriceRepo = prodPriceRepo;
            _nomPhotoRepo = nomPhotoRepo;

            _mapper = mapper;
            _userService = userService;
            _nomReadService = nomReadService;
        }

        public List<CModelDTO> GetNomModels()
        {
            var nomModels = _nomRepo.Query().Select(x => new
            {
                Id = x.id,
                IsVisible = x.is_visible,
                PhotoPath = x.image_path,
                Catalogs = x.NomCatalogs.Select(x => x.Catalog.name).ToList(),
                Categories = x.NomCategories.Select(x => x.Category.name).ToList()
            }).ToList();
            var nomList = _nomReadService.GetCatalogModels();
            var models = nomList
                .GroupJoin(nomModels,
                    nl => nl.Id,
                    nm => nm.Id,
                    (nomList, nomModels) => new CModelDTO
                    {
                        Id = nomList.Id,
                        Articul = nomList.Articul,
                        ItemName = nomList.ItemName,
                        Sizes = nomList.Sizes,
                        Colors = nomList.Colors,
                        IsVisible = nomModels.Select(x => x.IsVisible).SingleOrDefault(),
                        PhotoPath = nomModels.Select(x => x.PhotoPath).SingleOrDefault(),
                        Catalogs = nomModels.Select(x => x.Catalogs).SingleOrDefault(),
                        Categories = nomModels.Select(x => x.Categories).SingleOrDefault()
                    }).ToList();

            return models;
        }

        public List<NomUserDTO> GetNomUsers(UserDTO user, Guid nomId)
        {
            var existNomUser = _nomUserRepo.QueryFindBy(x => x.nom_id == nomId).Select(x => x.user_id).ToList();
            var users = _userService.GetAllUsers(user);
            var nomUsers = users.Select(x => new NomUserDTO
            {
                UserId = x.UserId,
                CityName = x.CityName,
                ClientFullName = x.ClientFullName,
                ClientLogin = x.Login,
                MallAddress = x.ShopName,
                NotVisible = existNomUser.Contains(x.UserId)
            }).ToList();
            return nomUsers;
        }

        public async Task<NomModelDTO> GetNomModel(Guid id)
        {
            var nomModel = await _nomRepo.FindByIdAsync(id);
            var dto = _mapper.Map<NomModelDTO>(nomModel);
            if (dto != null)
            {
                dto.Catalogs = (await _nomCatalogRepo.GetAllFindByAsync(x => x.nom_id == id)).Select(x => x.catalog_id).ToList();
                dto.Categories = (await _nomCategoryRepo.GetAllFindByAsync(x => x.nom_id == id)).Select(x => x.category_id).ToList();
            }

            return dto;
        }


        public async Task<OperationResult> AddNomModel(NomModelDTO dto)
        {
            try
            {
                var nomModel = _mapper.Map<Nomenclature>(dto);
                var existNomModel = await _nomRepo.FindByIdAsync(nomModel.id);
                if (existNomModel != null)
                {
                    nomModel.image_path = nomModel.image_path ?? existNomModel.image_path;
                    _nomRepo.DetachEntity(existNomModel);
                    _nomRepo.Update(nomModel);
                }
                else
                {
                    nomModel.is_visible = true;
                    _nomRepo.Add(nomModel);
                }
                await AddNomCatalogs(dto.Catalogs, nomModel.id);
                await AddNomCategories(dto.Categories, nomModel.id);
                await AddNomUsers(dto.Clients, nomModel.id);
                AddNomPhotos(dto.PhotoPaths, nomModel.id);
                await _nomRepo.SaveChangesAsync();

                return OperationResult.AlertSuccess("Успешно: процедура выполнилась");
            }
            catch (Exception ex)
            {
                return OperationResult.AlertFailure("Ошибка: процедура не выполнилась");
            }
        }

        private async Task AddNomCatalogs(List<Guid> catalogIds, Guid nomId)
        {
            var existNomCtlgs = await _nomCatalogRepo.GetAllFindByAsync(x => x.nom_id == nomId);
            _nomCatalogRepo.RemoveRange(existNomCtlgs.ToList());

            List<NomCatalog> nomCatalogs = new List<NomCatalog>();
            foreach (var catId in catalogIds)
            {
                var entity = new NomCatalog { nom_id = nomId, catalog_id = catId };
                var lastOrder = _nomCatalogRepo.QueryFindBy(x => x.catalog_id == catId)
                    .OrderByDescending(s => s.order)
                    .FirstOrDefault()?.order;
                if (lastOrder == null)
                    entity.order = 1;
                else
                    entity.order = lastOrder.Value + 1;
                nomCatalogs.Add(entity);
            }
            _nomCatalogRepo.AddRange(nomCatalogs);
        }

        private async Task AddNomCategories(List<Guid> categIds, Guid nomId)
        {
            var existNomCtgrs = await _nomCategoryRepo.GetAllFindByAsync(x => x.nom_id == nomId);
            _nomCategoryRepo.RemoveRange(existNomCtgrs.ToList());

            List<NomCategory> nomCategories = new List<NomCategory>();
            foreach (var catId in categIds)
            {
                nomCategories.Add(new NomCategory { nom_id = nomId, category_id = catId });
            }
            _nomCategoryRepo.AddRange(nomCategories);
        }

        private async Task AddNomUsers(List<Guid> userIds, Guid nomId)
        {
            var existNomUsers = await _nomUserRepo.GetAllFindByAsync(x => x.nom_id == nomId);
            _nomUserRepo.RemoveRange(existNomUsers.ToList());

            List<VisibleNomUser> nomUsers = new List<VisibleNomUser>();
            foreach (var usId in userIds)
            {
                nomUsers.Add(new VisibleNomUser { nom_id = nomId, user_id = usId });
            }
            _nomUserRepo.AddRange(nomUsers);
        }

        private void AddNomPhotos(List<string> photoPaths, Guid nomId)
        {
            List<NomPhoto> nomPhotos = new List<NomPhoto>();
            foreach (var path in photoPaths)
            {
                nomPhotos.Add(new NomPhoto { nom_id = nomId, photo_path = path });
            }
            _nomPhotoRepo.AddRange(nomPhotos);
        }


        public List<Dictionary<string, object>> GetProdCtlgs(Guid nomId, Guid ctlgId)
        {
            var productIds = _visProdCtlgRepo.QueryFindBy(x => x.nom_id == nomId && x.catalog_id == ctlgId)
                .Select(s => s.product_id).ToList();

            return _nomReadService.GetColorsSizesGrid(nomId, productIds);
        }

        public async Task AddVisProdCtlg(Guid nomId, Guid ctlgId, Guid prodId)
        {
            var prodCtlg = await _visProdCtlgRepo.FindByFilterAsync(x => x.nom_id == nomId && x.product_id == prodId && x.catalog_id == ctlgId);
            if (prodCtlg != null)
                _visProdCtlgRepo.Remove(prodCtlg);
            else
            {
                prodCtlg = new NotVisibleProdCtlg
                {
                    nom_id = nomId,
                    product_id = prodId,
                    catalog_id = ctlgId
                };
                _visProdCtlgRepo.Add(prodCtlg);
            }
            await _visProdCtlgRepo.SaveChangesAsync();
        }


        public List<Dictionary<string, object>> GetProdCtgrs(Guid nomId, Guid ctgrId)
        {
            var productIds = _visProdCtgrRepo.QueryFindBy(x => x.nom_id == nomId && x.category_id == ctgrId)
                .Select(s => s.product_id).ToList();

            return _nomReadService.GetColorsSizesGrid(nomId, productIds);
        }

        public async Task AddVisProdCtgr(Guid nomId, Guid ctgrId, Guid prodId)
        {
            var prodCtgr = await _visProdCtgrRepo.FindByFilterAsync(x => x.nom_id == nomId && x.product_id == prodId && x.category_id == ctgrId);
            if (prodCtgr != null)
                _visProdCtgrRepo.Remove(prodCtgr);
            else
            {
                prodCtgr = new NotVisibleProdCtgr
                {
                    nom_id = nomId,
                    product_id = prodId,
                    category_id = ctgrId
                };
                _visProdCtgrRepo.Add(prodCtgr);
            }
            await _visProdCtgrRepo.SaveChangesAsync();
        }


        public List<Dictionary<string, object>> GetPrices(Guid nomId)
        {
            var productPrices = _prodPriceRepo.QueryFindBy(x => x.nom_id == nomId).ToList();
            var nomList = _nomReadService.GetCModelGrid(nomId, new List<Guid>());

            var bindGrid = nomList
                .GroupJoin(productPrices,
                    nl => nl.ProductId,
                    pp => pp.product_id,
                    (nomList, productPrices) => new
                    {
                        nomList.ProductId,
                        nomList.Color,
                        nomList.Size,
                        Price = productPrices.Select(s => s.price).SingleOrDefault()
                    }).ToList();

            var sizes = bindGrid.Select(x => x.Size).Distinct().ToList();
            var groupColors = bindGrid.GroupBy(x => x.Color)
                .Select(x => new
                {
                    Сolor = x.Key,
                    Options = x.Select(d2 => new { d2.Size, d2.Price, d2.ProductId })
                }).ToList();

            var list = new List<Dictionary<string, object>>();
            foreach (var g in groupColors)
            {
                var dict = new Dictionary<string, object>();
                dict["Цвет"] = g.Сolor;
                foreach (var size in sizes)
                {
                    dict.Add(size, null);
                }
                foreach (var opt in g.Options)
                {
                    dict[opt.Size] = new OptAttributes(opt.ProductId, price: opt.Price);
                }
                list.Add(dict);
            }
            return list;
        }

        public async Task AddProdPrice(Guid nomId, Guid prodId, double price)
        {
            var prodPrice = await _prodPriceRepo.FindByFilterAsync(x => x.nom_id == nomId && x.product_id == prodId);
            if (prodPrice != null)
            {
                prodPrice.price = price;
                _prodPriceRepo.Update(prodPrice);
            }
            else
            {
                prodPrice = new NomProdPrice
                {
                    nom_id = nomId,
                    product_id = prodId,
                    price = price
                };
                _prodPriceRepo.Add(prodPrice);
            }
            await _prodPriceRepo.SaveChangesAsync();
        }


        public List<Dictionary<string, object>> GetSHAmounts(Guid nomId)
        {
            return _nomReadService.GetSHAmountGrid(nomId);
        }


        public async Task<List<PhotoDTO>> GetNomPhotos(Guid nomId)
        {
            var colors = _nomReadService.GetColorsByNomId(nomId);
            var photos = await _nomPhotoRepo.GetAllFindByAsync(x => x.nom_id == nomId);
            var dto = photos
                .GroupJoin(colors,
                    ph => ph.color_id,
                    cl => cl.Id,
                    (photos, colors) => new PhotoDTO
                    {
                        Id = photos.id,
                        PhotoPath = photos.photo_path,
                        ColorName = colors.Select(s => s.Name).SingleOrDefault()
                    }).ToList();
            return dto;
        }

        public async Task<OperationResult> AddColorPhoto(Guid photoId, Guid colorId)
        {
            NomPhoto photo = await _nomPhotoRepo.FindByIdAsync(photoId);
            if (colorId == Guid.Empty)
                photo.color_id = null;
            else
                photo.color_id = colorId;
            _nomPhotoRepo.Update(photo);
            await _nomPhotoRepo.SaveChangesAsync();
            return OperationResult.AlertSuccess("Успешно: процедура выполнилась");
        }

        public async Task<OperationResult> RemovePhoto(Guid photoId)
        {
            NomPhoto photo = await _nomPhotoRepo.FindByIdAsync(photoId);
            _nomPhotoRepo.Remove(photo);
            await _nomPhotoRepo.SaveChangesAsync();
            return OperationResult.AlertSuccess("Успешно: процедура выполнилась");
        }


        public List<CModelDTO> GetNomModelsByCatalog(Guid id)
        {
            var nomModels = _nomCatalogRepo.QueryFindBy(x => x.catalog_id == id)
                .Select(x => new
                {
                    Id = x.Nomenclature.id,
                    Order = x.order,
                    IsVisible = x.Nomenclature.is_visible,
                    PhotoPath = x.Nomenclature.image_path,
                    Catalogs = x.Nomenclature.NomCatalogs.Select(x => x.Catalog.name).ToList(),
                    Categories = x.Nomenclature.NomCategories.Select(x => x.Category.name).ToList()
                }).ToList();
            var nomList = _nomReadService.GetCatalogModels();
            var models = nomList
                .Join(nomModels,
                    nl => nl.Id,
                    nm => nm.Id,
                    (nomList, nomModels) => new CModelDTO
                    {
                        Id = nomList.Id,
                        Articul = nomList.Articul,
                        ItemName = nomList.ItemName,
                        Sizes = nomList.Sizes,
                        Colors = nomList.Colors,
                        Order = nomModels.Order,
                        IsVisible = nomModels.IsVisible,
                        PhotoPath = nomModels.PhotoPath,
                        Catalogs = nomModels.Catalogs,
                        Categories = nomModels.Categories
                    }).ToList();

            return models;
        }

        public async Task<OperationResult> AddModelsCtlg(Guid id, List<Guid> modelIds)
        {
            var existNoms = await _nomCatalogRepo.GetAllFindByAsync(x => x.catalog_id == id);
            _nomCatalogRepo.RemoveRange(existNoms.ToList());

            List<NomCatalog> nomCatalogs = new List<NomCatalog>();
            foreach (var nomId in modelIds)
            {
                nomCatalogs.Add(new NomCatalog { nom_id = nomId, catalog_id = id });
            }
            _nomCatalogRepo.AddRange(nomCatalogs);
            await _nomCatalogRepo.SaveChangesAsync();
            return OperationResult.AlertSuccess("Успешно: процедура выполнилась");
        }


        public List<CModelDTO> GetNomModelsByCategory(Guid id)
        {
            var nomCtgrs = _nomCategoryRepo.QueryFindBy(x => x.category_id == id).Select(s => s.nom_id);
            var nomModels = _nomRepo.QueryFindBy(x => x.is_visible).Select(x => new
            {
                Id = x.id,
                Catalogs = x.NomCatalogs.Select(x => x.Catalog.name).ToList(),
                Categories = x.NomCategories.Select(x => x.Category.name).ToList()
            }).ToList();
            var nomList = _nomReadService.GetCatalogModels();
            var models = nomList
                .Join(nomModels,
                    nl => nl.Id,
                    nm => nm.Id,
                    (nomList, nomModels) => new CModelDTO
                    {
                        Id = nomList.Id,
                        Articul = nomList.Articul,
                        ItemName = nomList.ItemName,
                        Sizes = nomList.Sizes,
                        Colors = nomList.Colors,
                        IsVisible = nomCtgrs.Contains(nomModels.Id),
                        Catalogs = nomModels.Catalogs,
                        Categories = nomModels.Categories
                    }).ToList();

            return models;
        }

        public async Task<OperationResult> AddModelsCtgr(Guid id, List<Guid> modelIds)
        {
            var existNoms = await _nomCategoryRepo.GetAllFindByAsync(x => x.category_id == id);
            _nomCategoryRepo.RemoveRange(existNoms.ToList());

            List<NomCategory> nomCategories = new List<NomCategory>();
            foreach (var nomId in modelIds)
            {
                nomCategories.Add(new NomCategory { nom_id = nomId, category_id = id });
            }
            _nomCategoryRepo.AddRange(nomCategories);
            await _nomCategoryRepo.SaveChangesAsync();
            return OperationResult.AlertSuccess("Успешно: процедура выполнилась");
        }


        public List<ColorDTO> GetColorsByNomId(Guid nomId)
        {
            return _nomReadService.GetColorsByNomId(nomId);
        }

        public async Task<CModelDTO> GetCatalogModel(Guid id)
        {
            return await _nomReadService.GetCatalogModel(id);
        }


        public async Task UpdateNomCatalog(Guid id, Guid catId, int toPosition)
        {
            var entity = await _nomCatalogRepo.QueryFindBy(x => x.nom_id == id && x.catalog_id == catId)
                .AsNoTracking()
                .FirstOrDefaultAsync();
            entity.order = toPosition;
            _nomCatalogRepo.Update(entity);
            await _nomCatalogRepo.SaveChangesAsync();
        }

        public async Task<OperationResult> UpdateNomCatalogOrders(Guid id, Guid catId, int toPosition)
        {
            if (toPosition == 0)
                return OperationResult.AlertFailure("Ошибка: процедура не выполнилась");

            var nomCatalogs = _nomCatalogRepo.QueryFindBy(x => x.catalog_id == catId && x.order >= toPosition)
                .OrderBy(x => x.order)
                .ToList();
            int tempPosition = toPosition;
            foreach (var nomctlg in nomCatalogs)
            {
                if (nomctlg.nom_id == id)
                    nomctlg.order = toPosition;
                else
                    nomctlg.order = ++tempPosition;
            }
            _nomCatalogRepo.UpdateRange(nomCatalogs.ToList());
            await _nomCatalogRepo.SaveChangesAsync();
            return OperationResult.AlertSuccess("Успешно: процедура выполнилась");
        }
    }
}
