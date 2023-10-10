//Lấy ds Language
$(document).ready(function () {
    // Gọi API để lấy danh sách ngôn ngữ và cập nhật dropdownlist
    $.ajax({
        url: '/api/Languages',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            var dropdown = $('#languageDropdown');
            dropdown.empty();
            dropdown.append($('<option>').val('').text('Select a language'));
            $.each(data, function (key, entry) {
                dropdown.append($('<option>').val(entry.id).text(entry.name));
            });
        }
    });
});
//Lấy ds Genre
$(document).ready(function () {
    // Gọi API để lấy danh sách ngôn ngữ và cập nhật dropdownlist
    $.ajax({
        url: '/api/Genres',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            var dropdown = $('#genreDropdown');
            dropdown.empty();
            dropdown.append($('<option>').val('').text('Select a genre'));
            $.each(data, function (key, entry) {
                dropdown.append($('<option>').val(entry.id).text(entry.name));
            });
        }
    });
});
//Lấy ds Publisher
$(document).ready(function () {
    // Gọi API để lấy danh sách ngôn ngữ và cập nhật dropdownlist
    $.ajax({
        url: '/api/Publisher',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            var dropdown = $('#publisherDropdown');
            dropdown.empty();
            dropdown.append($('<option>').val('').text('Select a publisher'));
            $.each(data, function (key, entry) {
                dropdown.append($('<option>').val(entry.id).text(entry.name));
            });
        }
    });
});//Lấy ds Discount
$(document).ready(function () {
    // Gọi API để lấy danh sách ngôn ngữ và cập nhật dropdownlist
    $.ajax({
        url: '/api/Discount',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            var dropdown = $('#discountDropdown');
            dropdown.empty();
            dropdown.append($('<option>').val('').text('Select a discount'));
            $.each(data, function (key, entry) {
                dropdown.append($('<option>').val(entry.id).text(entry.name));
            });
        }
    });
});