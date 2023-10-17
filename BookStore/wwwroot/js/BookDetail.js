$(document).ready(function () {
    $('.rating i').click(function () {
        var rating = $(this).data('rating');
        var selectedRating = $('#selectedRating').val();

        // Nếu ngôi sao đã được chọn, thì tắt tất cả ngôi sao từ ngôi sao đó trở về trước
        if (selectedRating == rating) {
            $('#selectedRating').val('');
            $(this).removeClass('selected');
            $(this).prevAll().removeClass('selected');
        } else {
            // Ngược lại, chọn ngôi sao và cập nhật giá trị
            $('#selectedRating').val(rating);
            $('.rating i').removeClass('selected');
            $(this).addClass('selected');
            $(this).prevAll().addClass('selected');
        }
    });

    $('#reviewForm').submit(function (event) {
        // Ngăn chặn form submit mặc định
        event.preventDefault();

        // Lấy giá trị số sao lớn nhất được chọn
        var maxRating = Math.max.apply(null, $('.rating i.selected').map(function () {
            return $(this).data('rating');
        }).get());

        // Sử dụng giá trị lớn nhất cho ô input 'rating' trước khi submit
        $('#selectedRating').val(maxRating);

        // Đây là nơi bạn có thể submit form hoặc xử lý dữ liệu khác theo yêu cầu của bạn.
        // Ví dụ: $('#reviewForm').submit();
    });
});
//Show review start at last

// Lấy tất cả các phần tử có class "star-rating"
const starRatings = document.querySelectorAll('.star-rating');

starRatings.forEach(starRating => {
    // Lấy giá trị số sao từ thuộc tính data-rating
    const rating = parseInt(starRating.getAttribute('data-rating'));

    // Xóa tất cả các nội dung bên trong phần tử "star-rating"
    starRating.innerHTML = '';

    // Tạo icon sao màu vàng dựa trên giá trị số sao
    for (let i = 1; i <= 5; i++) {
        const starIcon = document.createElement('i');
        starIcon.className = 'fas fa-star';

        // Thêm class 'rated' cho các sao đã được đánh giá (màu vàng)
        if (i <= rating) {
            starIcon.classList.add('rated');
            starIcon.style.color = 'gold'; // Đặt màu vàng
        }
        starRating.appendChild(starIcon);
    }
});
