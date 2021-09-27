var table;
$(document).ready(function () {
    table = $('.table').DataTable({
        "ajax": {
            "url": "/Category/LoadData",
            "type": "GET",
            "datatype": "json",
            "dataSrc": ""
        },
        "columns": [
            {
                "render": function (data, type, full, meta) {
                    let count = meta.row;
                    count = count + 1;
                    totalCount = count;
                    return count;
                }
            },
            { "data": "name" },
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
                    return '<a href="/Category/Edit/' + full.id + '" class="btn btn-sm btn-success mr-2"><i class="fas fa-edit"></i> Изменить</a>' +
                        '<a href="/Category/Delete/' + full.id + '" class="btn btn-sm btn-danger mr-2"><i class="fas fa-trash"></i> Удалить</a>' +
                        '<a href="/CModel/IndexByCategory/' + full.id + '" class="btn btn-sm btn-info"><i class="fas fa-eye"></i> Модели <span class="badge badge-light">' + full.nomCount + '</span></a>';
                }
            }
        ],
        "language": {
            "url": '/datatables.Russian.json'
        }
    });
});