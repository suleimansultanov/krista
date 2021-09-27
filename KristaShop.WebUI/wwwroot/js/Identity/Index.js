var table;
$(document).ready(function () {
    table = $('.table').DataTable({
        "ajax": {
            "url": "/Identity/LoadData",
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
            { "data": "login" },
            { "data": "clientFullName" },
            { "data": "cityName" },
            { "data": "phoneNumber" },
            { "data": "shopName" },
            { "data": "email" },
            { "data": "statusName" },
            {
                "data": "userId",
                "searchable": false,
                "sortable": false,
                "render": function (data, type, full, meta) {
                    return '<input type="button" class="btn btn-sm btn-info" onclick="LinkGenerate(\'' + full.userId + '\')" value="Ссылка"/>';
                }
            }
        ],
        "language": {
            "url": '/datatables.Russian.json'
        }
    });
});


function LinkGenerate(userId) {
    var url = "/Identity/CreateLink";
    $.ajax({
        type: "POST",
        url: url,
        data: { "userId": userId },
        success: function (data) {
            console.log(data);
            $('#link').val(data);
            $("#LinkModal").modal("show");
        }
    });
};