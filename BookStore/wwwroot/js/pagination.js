// JavaScript để thêm nút phân trang vào bảng
var table = document.querySelector('table');
var tbody = table.getElementById('table-pagination');
var rowsPerPage = 5; // Số hàng hiển thị trên 1 trang
var currentPage = 1;

// Tạo dữ liệu mẫu (có thể thay bằng dữ liệu thực tế)
var data = [
    // Thêm các dòng dữ liệu ở đây
];

// Hàm để tạo nút phân trang
function renderPagination() {
    var totalPages = Math.ceil(data.length / rowsPerPage);
    var pagination = document.getElementById('pagination');
    pagination.innerHTML = '';

    for (var i = 1; i <= totalPages; i++) {
        var pageLink = document.createElement('a');
        pageLink.href = 'javascript:void(0)';
        pageLink.innerText = i;

        if (i === currentPage) {
            pageLink.className = 'active';
        }

        pageLink.addEventListener('click', function () {
            currentPage = parseInt(this.innerText);
            renderTable();
        });

        pagination.appendChild(pageLink);
    }
}

// Hàm để tạo bảng dựa trên trang hiện tại
function renderTable() {
    tbody.innerHTML = '';
    var start = (currentPage - 1) * rowsPerPage;
    var end = start + rowsPerPage;
    var rowData = data.slice(start, end);

    rowData.forEach(function (item, index) {
        var row = tbody.insertRow(index);
        row.insertCell(0).innerText = item.no;
        row.insertCell(1).innerText = item.image;
        row.insertCell(2).innerText = item.productName;
        row.insertCell(3).innerText = item.description;
        row.insertCell(4).innerText = item.price;
        row.insertCell(5).innerText = item.author;
        row.insertCell(6).innerText = item.category;
        row.insertCell(7).innerText = item.dateAdded;
    });

    renderPagination();
}

// Gọi hàm renderTable() lần đầu để hiển thị bảng và phân trang
renderTable();