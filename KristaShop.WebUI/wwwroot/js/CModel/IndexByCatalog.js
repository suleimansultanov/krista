var table;
$(document).ready(function () {
    var id = $("#Id").val();
    table = $('.table').DataTable({
        "ajax": {
            "url": "/CModel/LoadDataByCatalog",
            "type": "GET",
            "datatype": "json",
            "data": {
                id: id
            },
            "dataSrc": ""
        },
        "processing": true,
        "rowReorder": {
            "dataSrc": 'order'
        },
        "columns": [
            { "data": "order", "className": 'reorder' },
            {
                "data": "photoPath",
                "width": "5%",
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
                "width": "10%"
            },
            {
                "data": "sizes",
                "width": "10%"
            },
            {
                "data": "catalogs",
                "width": "10%"
            },
            {
                "data": "categories",
                "width": "10%"
            },
            {
                "data": "isVisible",
                "width": "5%",
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
                "width": "25%",
                "render": function (data, type, full, meta) {
                    return '<a href="/CModel/Edit/' + data + '" class="btn btn-sm btn-success mr-2"><i class="fas fa-edit"></i> Изменить</a>' +
                        '<button type="button" onclick="ReorderModelModal(\'' + data + '\')" class="btn btn-sm btn-primary"><i class="fas fa-sort"></i> Упорядочить</button>';
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

    table.on('row-reorder.dt', function (e, diff, edit) {
        for (var i = 0; i < diff.length; i++) {
            var rowData = table.row(diff[i].node).data();
            $.ajax({
                type: "POST",
                url: '/CModel/UpdateRow',
                data: {
                    Id: rowData.id,
                    catId: id,
                    fromPosition: diff[i].oldData,
                    toPosition: diff[i].newData
                },
                dataType: "json"
            });
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

function ReorderModelModal(NomId) {
    $("#OrderNum").val("");
    $("#NomId").val(NomId);
    $("#reorder-model-modal").modal("show");
};

function ReorderModel() {
    var id = $("#Id").val();
    var nomId = $("#NomId").val();
    var orderNum = $("#OrderNum").val();
    $.ajax({
        type: 'POST',
        url: '/CModel/ReorderModel',
        data: {
            id: nomId,
            catId: id,
            toPosition: orderNum
        },
        dataType: 'json',
        success: function (alert) {
            showAlert(alert);
            table.ajax.reload(null, false);
            $("#reorder-model-modal").modal("hide");
        }
    });
};