var uri = 'http://localhost:10388/api/Account/Register';

function register() {
    var Email = $('#Email').val();
    var Password = $('#Password').val();
    var ConfirmPassword = $('#ConfirmPassword').val();

    
    var data = 'Email='.concat(Email, '&Password=', Password, '&ConfirmPassword=', ConfirmPassword);

   // var data = 'Email=asdfjkl@yahoo.com&Password=Password@123&ConfirmPassword=Password@123';

    $.post(uri, data);
}