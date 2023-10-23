
const bookId = localStorage.getItem("bookId");
$(document).ready(function () {
    const userId = localStorage.getItem("userId");
    const bookId = document.getElementById("bookId").value;

    // Handle form submission
    $('#reviewForm').submit(function (event) {
        event.preventDefault();

        const comment = $("#comment").val();
        const rating = $("#selectedRating").val();
        const date = new Date().toISOString();

        const reviewData = {
            userId: userId,
            comment: comment,
            rate: rating,
            date: date,
            bookId: bookId
        };

        // Make an AJAX POST request to submit the review
        $.ajax({
            url: apiUrl + '/api/Reviews',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(reviewData),
        })
            .done(function (response) {
                Swal.fire({
                    icon: 'success',
                    title: 'Your review has been saved',
                    showConfirmButton: false,
                    timer: 1500
                });
                $('#comment').val('');
                $('.rating i').removeClass('selected');
                setTimeout(function () {
                    location.reload();
                }, 1500);
            })
            .fail(function (xhr, status, error) {
                console.error(xhr);
                alert('An error occurred while sending data.');
            });
    });

    // Handle star ratings
    $('.rating i').click(function () {
        const rating = $(this).data('rating');
        const selectedRating = $("#selectedRating");

        if (selectedRating.val() == rating) {
            selectedRating.val(0);
            $(this).removeClass('selected');
            $(this).prevAll().removeClass('selected');
        } else {
            selectedRating.val(rating);
            $('.rating i').removeClass('selected');
            $(this).addClass('selected');
            $(this).prevAll().addClass('selected');
        }
    });

    // Show reviews based on rating
    const starRatings = document.querySelectorAll('.star-rating');

    starRatings.forEach(starRating => {
        const rating = parseInt(starRating.getAttribute('data-rating'));
        starRating.innerHTML = '';

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

function BtnMinus() {
    var input = document.getElementById("quantity");
    var currentValue = parseInt(input.value, 10);
    if (currentValue > 1) {
        input.value = currentValue - 1;
    }
}

function BtnPlus() {
    var input = document.getElementById("quantity");
    var currentValue = parseInt(input.value, 10);
    input.value = currentValue + 1;
}
