
document.addEventListener("DOMContentLoaded", () => {
    const apiUrl = localStorage.getItem("apiUrl")
    const today = new Date();
    const year = today.getFullYear();
    const month = (today.getMonth() + 1).toString().padStart(2, '0'); // Chuyển số tháng thành chuỗi và đảm bảo rằng nó có hai chữ số
    const day = today.getDate().toString().padStart(2, '0'); // Chuyển số ngày thành chuỗi và đảm bảo rằng nó có hai chữ số

    const formattedDate = `${year}-${month}-${day}`;
    $.ajax({
        url: apiUrl + "/api/Statistics?currentDate=" + formattedDate,
        method: "GET",
        success: function (data) {
            LineChart(data);
            BarChart(data);
            PieChart(data);
            TableBestSelling(data);
        },
        error: function (error) {
            console.log(error)
        },
    })
});

function LineChart(data) {
    var perDayOfMonth = data.perDayOfMonth;
    var label_dates = [];
    var revenueData = [];

    perDayOfMonth.forEach(item => {
        label_dates.push(item.date);
        revenueData.push(item.totalRevenue);
    });
    new ApexCharts(document.querySelector("#lineChart"), {
        series: [{
            name: "Desktops",
            data: revenueData
        }],
        chart: {
            height: 350,
            type: 'line',
            zoom: {
                enabled: false
            }
        },
        dataLabels: {
            enabled: false
        },
        stroke: {
            curve: 'straight'
        },
        grid: {
            row: {
                colors: ['#f3f3f3', 'transparent'], // takes an array which will be repeated on columns
                opacity: 0.5
            },
        },
        xaxis: {
            categories: label_dates,
        }
    }).render();
}

function BarChart(data) {
    var revenueByGenre = data.revenueByGenre;
    var label_genres = [];
    var revenueData = [];

    revenueByGenre.forEach(item => {
        label_genres.push(item.genreName);
        revenueData.push(item.totalRevenue);
    });

    var backgroundColors = [
        'rgba(255, 99, 132, 0.2)',
        'rgba(255, 159, 64, 0.2)',
        'rgba(255, 205, 86, 0.2)',
        'rgba(75, 192, 192, 0.2)',
        'rgba(54, 162, 235, 0.2)',
        'rgba(153, 102, 255, 0.2)',
        'rgba(201, 203, 207, 0.2)',
        'rgba(255, 0, 0, 0.2)',
        'rgba(0, 255, 0, 0.2)',
        'rgba(0, 0, 255, 0.2)',
        'rgba(128, 128, 0, 0.2)',
        'rgba(128, 0, 128, 0.2)'
    ];

    var borderColors = [
        'rgb(255, 99, 132)',
        'rgb(255, 159, 64)',
        'rgb(255, 205, 86)',
        'rgb(75, 192, 192)',
        'rgb(54, 162, 235)',
        'rgb(153, 102, 255)',
        'rgb(201, 203, 207)',
        'rgb(255, 0, 0)',
        'rgb(0, 255, 0)',
        'rgb(0, 0, 255)',
        'rgb(128, 128, 0)',
        'rgb(128, 0, 128)'
    ];
    new Chart(document.querySelector('#barChart'), {
        type: 'bar',
        data: {
            labels: label_genres,
            datasets: [{
                label: 'Bar Chart',
                data: revenueData,
                backgroundColor: backgroundColors,
                borderColor: borderColors,
                borderWidth: 2
            }]
        },
        options: {
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    });
}

function PieChart(data) {
    var revenueByPublisher = data.revenueByPublisher;
    var label_publishers = [];
    var revenueData = [];

    revenueByPublisher.forEach(item => {
        label_publishers.push(item.publisher);
        revenueData.push(item.totalRevenue);
    });

    new ApexCharts(document.querySelector("#pieChart"), {
        series: revenueData,
        chart: {
            height: 350,
            type: 'pie',
            toolbar: {
                show: true
            }
        },
        labels: label_publishers
    }).render();
}

function TableBestSelling(data) {
    var revenueBestSelling = data.bestSelling;
    var row = document.getElementById("table_bestselling");

    revenueBestSelling.forEach(item => {
        row.innerHTML += `<tr ><th scope="row"><a href="#"><img src="` + item.image + `" alt=""></a></th>
	<td><a href="#" class="text-primary fw-bold">`+ item.bookName + `</a></td>
	<td>$`+ item.price + `</td>
	<td class="fw-bold">`+ item.sold + `</td>
	<td>$`+ item.revenue + `</td></tr>	`
    });
}