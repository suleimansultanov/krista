$(".nav-tabs #users-tab").click(function () {
    var table = $('#user-table').DataTable({
        "ajax": {
            "url": "/Identity/LoadData",
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
            { "data": "login" },
            { "data": "clientFullName" },
            { "data": "cityName" },
            { "data": "phoneNumber" },
            { "data": "shopName" },
            { "data": "email" },
            { "data": "statusName" },
            { "data": "status", "visible": false },
            {
                "data": "userId",
                "searchable": false,
                "sortable": false,
                "width": "10%",
                "render": function (data, type, full, meta) {
                    return '<a href="/Discount/IndexType?discountId=' + data + '&discountType=3" class="btn btn-sm btn-info"><i class="fas fa-percent"></i> Скидка</a>';
                }
            }
        ],
        "destroy": true,
        "pageLength": -1,
        "lengthMenu": [[10, 50, 100, -1], [10, 50, 100, "Все"]],
        "dom": 'rt<"row"<"bottom col-sm-12 col-md-4 mt-3 text-left"l><"bottom col-sm-12 col-md-4 text-center"i><"bottom col-sm-12 col-md-4 col-auto"p>>',
        "language": {
            "url": '/datatables.Russian.json'
        }
    });

    $("#Login").keyup(function () {
        table
            .columns(1)
            .search($(this).val());
        table.draw();
    });

    $("#ClientName").keyup(function () {
        table
            .columns(2)
            .search($(this).val());
        table.draw();
    });

    $("#CityName").keyup(function () {
        table
            .columns(3)
            .search($(this).val());
        table.draw();
    });

    $("#PhoneNumber").keyup(function () {
        table
            .columns(4)
            .search($(this).val());
        table.draw();
    });

    $("#ShopName").keyup(function () {
        table
            .columns(5)
            .search($(this).val());
        table.draw();
    });

    $('#Status').change(function () {
        table
            .columns(8)
            .search($(this).val());
        table.draw();
    });
});

function ResetUserAllValues() {
    $('.card-body').find('.user-input:text').val('').trigger('keyup');
    $('.card-body').find('.selectpicker').val('').trigger('change');
}