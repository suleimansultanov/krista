$(document).ready(function () {
    var table = $('.table').DataTable({
        "ajax": {
            "url": "/Catalog/LoadData",
            "type": "GET",
            "datatype": "json",
            "dataSrc": ""
        },
        "rowReorder": {
            "dataSrc": 'order'
        },
        "columns": [
            { "data": "order", "className": 'reorder' },
            { "data": "name" },
            { "data": "uri" },
            { "data": "orderFormName" },
            {
                "data": "isVisible",
                "searchable": false,
                "sortable": false,
                "render": function (data, type, full, meta) {
                    if (data)
                        return '<i class="fa fa-eye text-success"></i>';
                    else
                        return '<i class="fa fa-eye text-danger"></i>';
                }
            },
            {
                "data": "id",
                "searchable": false,
                "width": "20%",
                "sortable": false,
                "render": function (data, type, full, meta) {
                    return '<a href="/Catalog/Edit/' + data + '" class="btn btn-sm btn-success mr-2"><i class="fas fa-edit"></i> Изменить</a>' +
                        '<a href="/Catalog/Delete/' + data + '" class="btn btn-sm btn-danger mr-2"><i class="fas fa-trash"></i> Удалить</a>' +
                        '<a href="/CModel/IndexByCatalog/' + data + '" class="btn btn-sm btn-info"><i class="fas fa-eye"></i> Модели <span class="badge badge-light">' + full.nomCount + '</span></a>';
                }
            }
        ],
        "language": {
            "url": '/datatables.Russian.json'
        }
    });

    table.on('row-reorder.dt', function (e, diff, edit) {
        for (var i = 0; i < diff.length; i++) {
            var rowData = table.row(diff[i].node).data();
            $.ajax({
                type: "POST",
                url: '/Catalog/UpdateRow',
                data: {
                    id: rowData.id,
                    fromPosition: diff[i].oldData,
                    toPosition: diff[i].newData
                },
                dataType: "json"
            });
        }
    });
});