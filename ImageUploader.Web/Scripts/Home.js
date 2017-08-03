function DeleteImage(imageGuid) {

    BootstrapDialog.show({
        title: 'Confirm',
        message: 'Are you sure you want to delete this image?',
        buttons: [{
            label: 'Yes',
            action: function (dialog) {
                window.location = "/Delete?fileId=" + imageGuid;
            }
        }, {
            label: 'No',
            action: function (dialog) {
                dialog.close();
            }
        }]
    });
}

function ShowFullImage(imageGuid) {
    BootstrapDialog.show({
        title: '',
        message: '<img class="fullimage" src="/File?fileId=' + imageGuid + '"/>',
        buttons: [{
            label: 'Close',
            action: function (dialog) {
                dialog.close();
            }
        }
        ]
    });
}