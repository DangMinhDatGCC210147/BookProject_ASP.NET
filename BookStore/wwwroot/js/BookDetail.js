
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

            if (i <= rating) {
                starIcon.classList.add('rated');
                starIcon.style.color = 'gold';
            }
            starRating.appendChild(starIcon);
        }
    });
    loadReviews(bookId);
});


function loadReviews(bookId) {
    const apiUrl = localStorage.getItem("apiUrl");
    $.ajax({
        url: apiUrl + '/api/Reviews/book/' + bookId,
        method: 'GET',
        dataType: 'json',
        success: function (data) {
            // Assuming data is an array of reviews
            const numberOfReviews = data.length;
            const totalReviewDiv = $('.description-review-title');
            totalReviewDiv.empty()
            totalReviewDiv.append(`<h3 style="font-weight: bold;">Reviews (${numberOfReviews})</h3>`)
            document.addEventListener("DOMContentLoaded", function () {
                // Mã JavaScript của bạn ở đây
            });
            const reviewContainer = $('#review-container');
            reviewContainer.empty();
            console.log(data)
            data.forEach(function (review) {
                const reviewDiv = $('<div class="review"></div>');

                const userCommentDiv = $('<div class="user-comment"></div>');

                userCommentDiv.append(`<div class="user-name">${review.user.firstName} ${review.user.lastName}</div>`);
                userCommentDiv.append(`<div class="star-rating" data-rating="${review.rate}">${getStarIcons(review.rate)}</div>`);
                userCommentDiv.append(`<p>${review.comment}</p>`);

                const reviewDateDiv = $(`<div class="review-date">${review.date}</div>`);

                reviewDiv.append(userCommentDiv);
                reviewDiv.append(reviewDateDiv);

                reviewContainer.append(reviewDiv);
            });
        },
        error: function (error) {
            console.error('Error loading reviews:', error);
        }
    });
}


function getStarIcons(rate) {
    const goldColor = 'gold'; // Màu vàng
    const greyColor = 'grey'; // Màu vàng
    const fullStar = `<i class="fas fa-star" style="color: ${goldColor};"></i>`;
    const halfStar = `<i class="fas fa-star-half-alt" style="color: ${goldColor};"></i>`;
    const emptyStar = `<i class="fas fa-star" style="color: ${greyColor};"></i>`;

    const stars = [];
    const fullStarsCount = Math.floor(rate);
    const hasHalfStar = rate % 1 !== 0;

    for (let i = 0; i < fullStarsCount; i++) {
        stars.push(fullStar);
    }

    if (hasHalfStar) {
        stars.push(halfStar);
    }

    while (stars.length < 5) {
        stars.push(emptyStar);
    }

    return stars.join('');
}