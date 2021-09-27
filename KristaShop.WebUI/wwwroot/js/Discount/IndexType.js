var table;
$(document).ready(function () {
    var id = $('#DiscountId').val();
    var type = $('#DiscountType').val();
    table = $('.table').DataTable({
        "ajax": {
            "url": "/Discount/LoadDataType",
            "data": {
                discountId: id,
                discountType: type
            },
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
            { "data": "discountPrice" },
            {
                "data": "isActive",
                "searchable": false,
                "sortable": false,
                "render": function (data, type, full, meta) {
                    if (data)
                        return '<i class="fa fa-toggle-on text-success"></i>';
                    else
                        return '<i class="fa fa-toggle-off text-danger"></i>';
                }
            },
            { "data": "startDate" },
            { "data": "endDate" },
            {
                "data": "id",
                "searchable": false,
                "sortable": false,
                "render": function (data, type, full, meta) {
                    return '<button class="btn btn-sm btn-success mr-2" onclick="Edit(\'' + data + '\',' + full.discountType + ')"><i class="fas fa-edit"></i> Изменить</button>' +
                        '<button class="btn btn-sm btn-danger" onclick="Delete(\'' + data + '\',' + full.discountType + ')"><i class="fas fa-trash"></i> Удалить</button>';
                }
            }
        ],
        "language": {
            "url": '/datatables.Russian.json'
        }
    });
});


$('#create-edit-modal').on('hidden.bs.modal', function (e) {
    $(this).find('form')[0].reset();
});
createOrEditSuccess = function (alert) {
    showAlert(alert);
    table.ajax.reload(null, false);
    $("#create-edit-modal").modal("hide");
};
createOrEditError = function (alert) {
    showAlert(alert.responseJSON);
    $("#create-edit-modal").modal("hide");
};


function Edit(id, type) {
    var url = "/Discount/Details";
    $.ajax({
        type: "GET",
        url: url,
        data: {
            entityId: id,
            entityType: type
        },
        success: function (data) {
            console.log(data);
            console.log(moment(data.startDate).format('MM/DD/YYYY'));
            $('#Id').val(data.id);
            $('#DiscountType').val(type);
            $('#IsActive').prop("checked", data.isActive);
            $('#DiscountPrice').val(data.discountPrice);
            $('#StartDate').val(moment(data.startDate).format('YYYY-MM-DD'));
            $('#EndDate').val(moment(data.endDate).format('YYYY-MM-DD'));
            $("#create-edit-modal").modal("show");
        }
    });
}

function Delete(id, type) {
    $('#EntityId').val(id);
    $('#EntityType').val(type);
    $("#delete-modal").modal("show");
}
deleteSuccess = function (alert) {
    showAlert(alert);
    table.ajax.reload(null, false);
    $("#delete-modal").modal("hide");
};
