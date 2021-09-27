var tablePhoto;
$(document).ready(function () {
    var nomId = $("#NomId").val();
    tablePhoto = $('#photos-table').DataTable({
        "ajax": {
            "url": "/CModel/LoadPhotos",
            "type": "GET",
            "datatype": "json",
            "data": {
                "id": nomId
            },
            "dataSrc": ""
        },
        "columns": [
            {
                "render": function (data, type, full, meta) {
                    let count = meta.row;
                    count = count + 1;
                    return count;
                }
            },
            {
                "data": "photoPath",
                "searchable": false,
                "sortable": false,
                "render": function (data, type, full, meta) {
                    return '<img src="' + data + '?size=30" alt="Alternate Text" />';
                }
            },
            { "data": "colorName" },
            {
                "data": "id",
                "searchable": false,
                "sortable": false,
                "render": function (data, type, full, meta) {
                    return '<button type="button" class="btn btn-sm btn-success mr-2" onclick="PhotoEdit(\'' + data + '\')"><i class="fas fa-edit"></i> Изменить</button>' +
                        '<button type="button" class="btn btn-sm btn-danger" onclick="PhotoDelete(\'' + data + '\')"><i class="fas fa-trash"></i> Удалить</button>';
                }
            }
        ],
        "language": {
            "url": '/datatables.Russian.json'
        }
    });
});

function PhotoEdit(id) {
    $('#PhotoId').val(id);
    $("#edit-photo-modal").modal("show");
}

function EditPhotoEvent() {
    var nomId = $("#NomId").val();
    var photoId = $('#PhotoId').val();
    var colorId = $('#ColorId').val();
    var url = "/CModel/EditPhoto";
    $.ajax({
        type: "POST",
        url: url,
        data: {
            nomId: nomId,
            photoId: photoId,
            colorId: colorId
        },
        success: function (alert) {
            showAlert(alert);
            tablePhoto.ajax.reload(null, false);
            $("#edit-photo-modal").modal("hide");
        }
    });
}

function PhotoDelete(id) {
    var url = "/CModel/DeletePhoto";
    $.ajax({
        type: "POST",
        url: url,
        data: {
            photoId: id
        },
        success: function (alert) {
            showAlert(alert);
            tablePhoto.ajax.reload(null, false);
        }
    });
}
