function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#Image-img').attr('src', e.target.result);
        }
        reader.readAsDataURL(input.files[0]);
    }
};
$("#Image").change(function () {
    readURL(this);
});

function ChangeVideo() {
    var videoUrl = $("#VideoUrl").val();
    var correctUrl = videoUrl.replace("watch?v=", "embed/");
    $("#VideoUrl").val(correctUrl);
    document.getElementById("videoFrame").src = correctUrl;
}


$('.custom-file-input').change(function (e) {
    var files = [];
    for (var i = 0; i < $(this)[0].files.length; i++) {
        files.push($(this)[0].files[i].name);
    }
    $(this).next('.custom-file-label').html(files.join(', '));
});