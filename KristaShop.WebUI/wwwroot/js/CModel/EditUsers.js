var table;
$(document).ready(function () {
    var nomId = $("#NomId").val();
    table = $('#users-table').DataTable({
        "ajax": {
            "url": "/CModel/LoadUsers",
            "type": "GET",
            "datatype": "json",
            "data": {
                "id": nomId
            },
            "dataSrc": ""
        },
        "columns": [
            {
                "data": "userId",
                "targets": 0,
                "searchable": false,
                "orderable": false,
                "className": 'dt-body-center',
                "render": function (data, type, full, meta) {
                    if (full.notVisible)
                        return '<input type="checkbox" checked="checked" id="Clients" name="Clients" value="' + $('<div/>').text(data).html() + '">';
                    else
                        return '<input type="checkbox" id="Clients" name="Clients" value="' + $('<div/>').text(data).html() + '">';
                }
            },
            { "data": "clientFullName" },
            { "data": "clientLogin" },
            { "data": "cityName" },
            { "data": "mallAddress" },
            {
                "data": "notVisible",
                "sortable": false,
                "render": function (data, type, full, meta) {
                    if (data)
                        return '<i class="fa fa-eye text-danger"></i>';
                    else
                        return '<i class="fa fa-eye text-success"></i>';
                }
            }
        ],
        'select': {
            'style': 'multi'
        },
        'order': [[1, 'asc']],
        "language": {
            "url": '/datatables.Russian.json'
        }
    });

    $('#example-select-all').on('click', function () {
        // Get all rows with search applied
        var rows = table.rows({ 'search': 'applied' }).nodes();
        // Check/uncheck checkboxes for all rows in the table
        $('input[type="checkbox"]', rows).prop('checked', this.checked);
    });

    // Handle click on checkbox to set state of "Select all" control
    $('#users-table tbody').on('change', 'input[type="checkbox"]', function () {
        // If checkbox is not checked
        if (!this.checked) {
            var el = $('#example-select-all').get(0);
            // If "Select all" control is checked and has 'indeterminate' property
            if (el && el.checked && ('indeterminate' in el)) {
                // Set visual state of "Select all" control
                // as 'indeterminate'
                el.indeterminate = true;
            }
        }
    });
});
