$("#NomCategories").change(function () {
    var nomId = $("#NomId").val();
    var ctgrId = $("#NomCategories").val();
    $.ajax({
        type: 'GET',
        url: '/CModel/LoadProdCtgr',
        data: {
            nomId: nomId,
            ctgrId: ctgrId
        },
        dataType: 'json',
        success: function (data) {
            generateCtgrDynamicTable(data);
        }
    });
});

function AddVisProdCtgr(prodId) {
    var nomId = $("#NomId").val();
    var ctgrId = $("#NomCategories").val();
    $.ajax({
        type: 'POST',
        url: '/CModel/ChangeVisProdCtgr',
        data: {
            nomId: nomId,
            ctgrId: ctgrId,
            prodId: prodId
        },
        dataType: 'json',
        success: function () {
            $.ajax({
                type: 'GET',
                url: '/CModel/LoadProdCtgr',
                data: {
                    nomId: nomId,
                    ctgrId: ctgrId
                },
                dataType: 'json',
                success: function (data) {
                    generateCtgrDynamicTable(data);
                }
            });
        }
    });
}

function generateCtgrDynamicTable(data) {
    $("#div-ctgrs").html("");
    $.makeTable = function (data, orderToRespect) {
        var table = $('<table class="table table-striped table-bordered text-center" >');
        var tblHeader = "<tr>";
        orderToRespect.forEach(function (ele, index) {
            tblHeader += "<th>" + ele + "</th>";
        });
        tblHeader += "</tr>";
        $(tblHeader).appendTo(table);
        $.each(data, function (index, value) {
            var TableRow = "<tr>";
            orderToRespect.forEach(function (ele, index) {
                if (value[ele] !== null) {
                    if (index != 0) {
                        if (value[ele].notVisible)
                            TableRow += '<td><button type="button" onclick="AddVisProdCtgr(\'' + value[ele].productId + '\')" class="btn btn-sm"><i class="fa fa-eye text-danger"></i></button></td>';
                        else
                            TableRow += '<td><button type="button" onclick="AddVisProdCtgr(\'' + value[ele].productId + '\')" class="btn btn-sm"><i class="fa fa-eye text-success"></i></button></td>';
                    }
                    else {
                        TableRow += "<td>" + value[ele] + "</td>";
                    }
                }
                else {
                    TableRow += "<td bgcolor='silver'></td>";
                }
            });
            TableRow += "</tr>";
            $(table).append(TableRow);
        });
        return ($(table));
    };
    var orderKey = Object.keys(data[0]);

    var filteredAry = orderKey.filter(e => e !== 'Цвет')
    filteredAry.unshift("Цвет");
    var table = $.makeTable(data, filteredAry);

    $(table).appendTo("#div-ctgrs");
}