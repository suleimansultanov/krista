var table;
$(document).ready(function () {
    table = $('.table').DataTable({
        "ajax": {
            "url": "/Menu/LoadData",
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
            { "data": "title" },
            { "data": "controllerName" },
            { "data": "actionName" },
            { "data": "icon" },
            { "data": "params" },
            { "data": "order" },
            {
                "data": "id",
                "searchable": false,
                "sortable": false,
                "render": function (data, type, full, meta) {
                    return '<a href="/Menu/Edit/' + full.id + '" class="btn btn-sm btn-success mr-2"><i class="fas fa-edit"></i> Изменить</a>' +
                        '<a href="/Menu/Delete/' + full.id + '" class="btn btn-sm btn-danger"><i class="fas fa-trash"></i> Удалить</a>';
                }
            }
        ],
        "language": {
            "url": '/datatables.Russian.json'
        }
    });
});