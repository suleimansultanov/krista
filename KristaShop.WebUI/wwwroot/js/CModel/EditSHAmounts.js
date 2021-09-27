$(".nav-tabs #shamount-tab").click(function () {
    var nomId = $("#NomId").val();
    $.ajax({
        type: 'GET',
        url: '/CModel/LoadSHAmount',
        data: {
            nomId: nomId
        },
        dataType: 'json',
        success: function (data) {
            console.log(data);
            if (data.length !== 0)
                generateSHAmountDynamicTable(data);
        }
    });
});

function generateSHAmountDynamicTable(data) {
    $("#div-shamounts").html("");
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
                    TableRow += "<td>" + value[ele] + "</td>";
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

    $(table).appendTo("#div-shamounts");
}