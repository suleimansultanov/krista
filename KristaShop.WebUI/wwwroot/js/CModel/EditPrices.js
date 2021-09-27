$(".nav-tabs #price-tab").click(function () {
    var nomId = $("#NomId").val();
    $.ajax({
        type: 'GET',
        url: '/CModel/LoadProdPrice',
        data: {
            nomId: nomId
        },
        dataType: 'json',
        success: function (data) {
            generatePriceDynamicTable(data);
        }
    });
});

function AddProdPrice(prodId) {
    var nomId = $("#NomId").val();
    var price = $('#price' + prodId + '').val();
    $('#btn' + prodId + '').attr("disabled", true);
    $.ajax({
        type: 'POST',
        url: '/CModel/ChangeProdPrice',
        data: {
            nomId: nomId,
            prodId: prodId,
            price: price
        },
        dataType: 'json',
        success: function () {
            $.ajax({
                type: 'GET',
                url: '/CModel/LoadProdPrice',
                data: {
                    nomId: nomId
                },
                dataType: 'json',
                success: function (data) {
                    generatePriceDynamicTable(data);
                    $('#btn' + prodId + '').removeAttr("disabled");
                }
            });
        }
    });
}

function generatePriceDynamicTable(data) {
    $("#div-prices").html("");
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
                        TableRow += '<td><div class="input-group">' +
                            '<div class="input-group-prepend">' +
                            '<span class="input-group-text">$</span>' +
                            '</div>' +
                            '<input id="price' + value[ele].productId + '" value="' + value[ele].price + '" type="text" class="form-control" aria-label="Amount">' +
                            '<div class="input-group-append">' +
                            '<button id="btn' + value[ele].productId + '" type="button" onclick="AddProdPrice(\'' + value[ele].productId + '\')" class="btn btn-sm btn-default"><i class="fa fa-check text-success"></i></button>'
                        '</div></div></td>';
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

    $(table).appendTo("#div-prices");
}