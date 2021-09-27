var table;
$(document).ready(function () {
    table = $('.table').DataTable({
        "ajax": {
            "url": "/CModel/LoadData",
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
            {
                "data": "photoPath",
                "searchable": false,
                "sortable": false,
                "render": function (data, type, full, meta) {
                    return '<img src="' + data + '?size=100" alt="Alternate Text" />';
                }
            },
            {
                "data": "articul",
                "width": "5%"
            },
            {
                "data": "itemName",
                "width": "10%"
            },
            {
                "data": "colors",
                "width": "15%"
            },
            {
                "data": "sizes",
                "width": "15%"
            },
            {
                "data": "catalogs",
                "width": "15%"
            },
            {
                "data": "categories",
                "width": "15%"
            },
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
                "sortable": false,
                "width": "10%",
                "render": function (data, type, full, meta) {
                    return '<a href="/CModel/Edit/' + data + '" class="btn btn-sm btn-success mr-2"><i class="fas fa-edit"></i> Изменить</a>';
                }
            }
        ],
        "pageLength": -1,
        "lengthMenu": [[10, 50, 100, -1], [10, 50, 100, "Все"]],
        "dom": 'rt<"row"<"bottom col-sm-12 col-md-4 mt-3 text-left"l><"bottom col-sm-12 col-md-4 text-center"i><"bottom col-sm-12 col-md-4 col-auto"p>>',
        "language": {
            "url": '/datatables.Russian.json'
        }
    });

    $("#Articul").keyup(function () {
        table
            .columns(2)
            .search($(this).val());
        table.draw();
    });

    $('#Color').change(function () {
        table
            .columns(4)
            .search($(this).val());
        table.draw();
    });

    $('#Size').change(function () {
        table
            .columns(5)
            .search($(this).val());
        table.draw();
    });

    $('#SizeLine').change(function () {
        table
            .columns(5)
            .search($(this).val());
        table.draw();
    });

    $('#Catalog').change(function () {
        table
            .columns(6)
            .search($(this).val());
        table.draw();
    });

    $('#Category').change(function () {
        table
            .columns(7)
            .search($(this).val());
        table.draw();
    });
});

function ResetAllValues() {
    $('.card-body').find('input:text').val('').trigger('keyup').focus();
    $('.card-body').find('.selectpicker').val('').trigger('change');
}