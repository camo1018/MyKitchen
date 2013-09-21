$(function() {
    $('#buttonRegister').on('click', function() {
        var pattern = new RegExp("[A-z0-9]{4,}");
        if (pattern.test($('#inputUsername').val())) {
            var username = $('#inputUsername').val();
            var password = $('#inputPassword').val();

            $.ajax({
                url: "/Users/AddUser",
                type: 'post',
                data: { username: username, password: password },
                context: this,
                success: function() {
                    $('#failureMessage').css('display', 'none');
                    $('#successMessage').css('display', '');
                }
            });
        } else {
            $('#successMessage').css('display', 'none');
            $('#failureMessage').css('display', '');
        }
    });
})