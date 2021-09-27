$(document).ready(function () {
    var table = $('#catalog-table').DataTable({
        "ajax": {
            "url": "/Catalog/LoadData",
            "type": "GET",
            "datatype": "json",
            "dataSrc": ""
        },
        "processing": true,
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
                "width": "10%",
                "sortable": false,
                "render": function (data, type, full, meta) {
                    return '<a href="/Discount/IndexType?discountId=' + data + '&discountType=1" class="btn btn-sm btn-info"><i class="fas fa-percent"></i> Скидка</a>';
                }
            }
        ],
        "pageLength": -1,
        "lengthMenu": [[10, 50, 100, -1], [10, 50, 100, "Все"]],
        "language": {
            "url": '/datatables.Russian.json'
        }
    });
});