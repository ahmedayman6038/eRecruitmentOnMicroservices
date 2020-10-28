function BuildBarChart(element, labels, values, chartTitle, dataLabel) {
    var ctx = document.getElementById(element).getContext('2d');
    return new Chart(ctx, {
        type: 'bar',
        data: {
            labels: labels,
            datasets: [
                {
                    label: dataLabel,
                    backgroundColor: function (value) {
                        if (value.dataIndex % 2 == 0) {
                            return '#3e95cd';
                        }
                        return '#8e5ea2';
                    },
                    data: values
                }
            ]
        },
        options: {
            legend: { display: false },
            title: {
                display: true,
                text: chartTitle
            },
            scales: {
                yAxes: [{
                    ticks: {
                        beginAtZero: true,
                        precision: 0
                    }
                }]
            }
        }
    });
}

function BuildPieChart(element, labels, values, chartTitle, dataLabel) {
    var ctx = document.getElementById(element).getContext('2d');
    return new Chart(ctx, {
        type: 'pie',
        data: {
            labels: labels,
            datasets: [
                {
                    label: dataLabel,
                    backgroundColor: ["#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850"],
                    data: values
                }
            ]
        },
        options: {
            title: {
                display: true,
                text: chartTitle
            }
        }
    });
}