var table;
$(document).ready(function () {
    table = $('.table').DataTable({
        "ajax": {
            "url": "/Setting/LoadData",
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
            { "data": "key" },
            { "data": "value" },
            {
                "data": "id",
                "searchable": false,
                "sortable": false,
                "render": function (data, type, full, meta) {
                    return '<button class="btn btn-sm btn-success mr-2" onclick="Edit(\'' + data + '\')"><i class="fas fa-edit"></i> Изменить</button>' +
                        '<button class="btn btn-sm btn-danger" onclick="Delete(\'' + data + '\')"><i class="fas fa-trash"></i> Удалить</button>';
                }
            }
        ],
        "language": {
            "url": '/datatables.Russian.json'
        }
    });
});


$('#create-modal').on('hidden.bs.modal', function (e) {
    $(this).find('form')[0].reset();
});
createSuccess = function (alert) {
    showAlert(alert);
    table.ajax.reload(null, false);
    $("#create-modal").modal("hide");
};
createError = function (alert) {
    showAlert(alert.responseJSON);
    $("#create-modal").modal("hide");
};


function Edit(id) {
    var url = "/Setting/Details?id=" + id;
    $.ajax({
        type: "GET",
        url: url,
        success: function (data) {
            console.log(data);
            $('#IdEdit').val(data.id);
            $('#KeyEdit').val(data.key);
            $('#ValueEdit').val(data.value);
            $("#edit-modal").modal("show");
        }
    });
}
editSuccess = function (alert) {
    showAlert(alert);
    table.ajax.reload(null, false);
    $("#edit-modal").modal("hide");
};
editError = function (alert) {
    showAlert(alert.responseJSON);
    $("#edit-modal").modal("hide");
};


function Delete(id) {
    $('#Id').val(id);
    $("#delete-modal").modal("show");
}
deleteSuccess = function (alert) {
    showAlert(alert);
    table.ajax.reload(null, false);
    $("#delete-modal").modal("hide");
};
