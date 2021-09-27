var table;
$(document).ready(function () {
    table = $('.table').DataTable({
        "ajax": {
            "url": "/MBody/LoadData",
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
            { "data": "url" },
            { "data": "layout" },
            { "data": "metaTitle" },
            { "data": "metaDescription" },
            { "data": "metaKeywords" },
            {
                "data": "id",
                "searchable": false,
                "sortable": false,
                "render": function (data, type, full, meta) {
                    return '<a href="/MBody/Edit/' + full.id + '" class="btn btn-sm btn-success mr-2"><i class="fas fa-edit"></i> Изменить</a>' +
                        '<a href="/MBody/Delete/' + full.id + '" class="btn btn-sm btn-danger"><i class="fas fa-trash"></i> Удалить</a>';
                }
            }
        ],
        "language": {
            "url": '/datatables.Russian.json'
        }
    });
});
