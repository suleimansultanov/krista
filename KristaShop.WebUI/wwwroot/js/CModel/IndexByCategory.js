var table;
$(document).ready(function () {
    var id = $("#Id").val();
    table = $('.table').DataTable({
        "ajax": {
            "url": "/CModel/LoadDataByCategory",
            "type": "GET",
            "datatype": "json",
            "data": {
                id: id
            },
            "dataSrc": ""
        },
        "columns": [
            {
                "data": "id",
                "targets": 0,
                "searchable": false,
                "orderable": false,
                "className": 'dt-body-center',
                "render": function (data, type, full, meta) {
                    if (full.isVisible)
                        return '<input type="checkbox" checked="checked" id="modelIds" name="modelIds" value="' + $('<div/>').text(data).html() + '">';
                    else
                        return '<input type="checkbox" id="modelIds" name="modelIds" value="' + $('<div/>').text(data).html() + '">';
                }
            },
            {
                "render": function (data, type, full, meta) {
                    let count = meta.row;
                    count = count + 1;
                    totalCount = count;
                    return count;
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
                "width": "20%"
            },
            {
                "data": "categories",
                "width": "20%"
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
    $('.table tbody').on('change', 'input[type="checkbox"]', function () {
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

createSuccess = function (alert) {
    showAlert(alert);
    table.ajax.reload(null, false);
};
createError = function (alert) {
    showAlert(alert.responseJSON);
};