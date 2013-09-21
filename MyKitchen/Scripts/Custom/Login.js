$(function() {
    $('#buttonLogin').on('click', function () {
        var username = $('#inputUsername').val();
        var password = $('#inputPassword').val();
        $.ajax({
            url: "/Users/Login",
            type: 'post',
            data: { username: username, password: password },
            context: this,
            success: function(data) {
                var validLogin = data;
                if (validLogin) {
                    $('#failureMessage').css('display', 'none');
                    alert("Logged in!");
                } else {
                    $('#failureMessage').css('display', '');
                }
            }
        });
    });
})