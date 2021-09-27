function showAlert(alert) {
    const alertContainer = $('.alert-container');

    const alertElement = $(`<div class="alert alert-${alert.alertType} alert-dismissible" role="alert">` +
        '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>' +
        `<strong>${alert.alertMessage}</strong>` +
        '</div>');

    alertContainer.append(alertElement);
    alertElement.alert();
}