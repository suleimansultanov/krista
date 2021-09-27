function format(d) {
    // `d` is the original data object for the row
    return '<table cellpadding="5" cellspacing="0" border="0" style="padding-left:50px;">' +
        '<tr>' +
        '<td><b>Текст:</b></td>' +
        '<td>' + d.message + '</td>' +
        '</tr>' +
        '<tr>' +
        '</table>';
}

$(document).ready(function () {
    var table = $('.table').DataTable({
        "ajax": {
            "url": "/Feedback/LoadData",
            "type": "GET",
            "datatype": "json",
            "dataSrc": ""
        },
        "columns": [
            {
                "className": 'details-control',
                "orderable": false,
                "data": null,
                "defaultContent": ''
            },
            {
                "render": function (data, type, full, meta) {
                    let count = meta.row;
                    count = count + 1;
                    totalCount = count;
                    return count;
                }
            },
            { "data": "person" },
            { "data": "phone" },
            { "data": "email" },
            { "data": "recordTimeStamp" },
            {
                "data": "viewed",
                "render": function (data, type, full, meta) {
                    if (data)
                        return '<i class="fa fa-eye text-success"></i>';
                    else
                        return '<i class="fa fa-eye text-muted"></i>';
                }
            },
            {
                "data": "id",
                "searchable": false,
                "sortable": false,
                "render": function (data, type, full, meta) {
                    console.log(full);
                    if (full.viewed)
                        return '';
                    else
                        return '<button class="btn btn-sm btn-success mr-2" onclick="Edit(\'' + data + '\')"><i class="fas fa-eye"></i> Прочитано</button>';
                }
            }
        ],
        "order": [[1, 'asc']],
        "language": {
            "url": '/datatables.Russian.json'
        }
    });

    $('.table tbody').on('click', 'td.details-control', function () {
        var tr = $(this).closest('tr');
        var row = table.row(tr);

        if (row.child.isShown()) {
            // This row is already open - close it
            row.child.hide();
            tr.removeClass('shown');
        }
        else {
            // Open this row
            row.child(format(row.data())).show();
            tr.addClass('shown');
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
    var url = "/Feedback/Edit";
    $.ajax({
        type: "POST",
        url: url,
        data: {
            id: id
        },
        success: function (data) {
            showAlert(data);
            table.ajax.reload(null, false);
        }
    });
}
