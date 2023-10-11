const apiUrl = localStorage.getItem("apiUrl")
// Gọi yêu cầu Ajax để lấy danh sách Genre
function updateGenreRequests() {
    $.ajax({
        url: '/api/Genres',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            const newRequestList = document.querySelector('.dropdown-menu.notifications');
            const pendingGenres = data.filter(genre => genre.approvalStatus === 0);
            const pendingRequestCount = pendingGenres.length;
            const badgeSpan = document.querySelector('.badge-number');
            badgeSpan.textContent = pendingRequestCount;

            // Remove existing items
            newRequestList.innerHTML = '';

            // Show the count in the dropdown header
            const headerItem = document.createElement('li');
            headerItem.classList.add('dropdown-header');
            headerItem.textContent = `You have ${pendingRequestCount} new requests`;
            newRequestList.appendChild(headerItem);

            // Loop through pendingGenres and add items
            pendingGenres.forEach(function (genre) {
                const listItem = document.createElement('li');
                listItem.classList.add('notification-item');
                const icon = document.createElement('i');
                icon.classList.add('bi', 'bi-info-circle', 'text-primary');
                const infoDiv = document.createElement('div');
                const title = document.createElement('h4');
                title.textContent = genre.name;
                const description = document.createElement('p');
                description.textContent = genre.description;
                //const time = document.createElement('p');
                //time.textContent = genre.time;
                infoDiv.appendChild(title);
                infoDiv.appendChild(description);
                //infoDiv.appendChild(time);
                listItem.appendChild(icon);
                listItem.appendChild(infoDiv);
                newRequestList.appendChild(listItem);
            });
        },
        error: function (xhr, status, error) {
            console.error(error);
        }
    });
}

// Tự động làm mới sau mỗi 5 phút (300,000 milliseconds)
setInterval(updateGenreRequests, 2000);

// Gọi hàm cập nhật ban đầu khi trang web được nạp
updateGenreRequests();


