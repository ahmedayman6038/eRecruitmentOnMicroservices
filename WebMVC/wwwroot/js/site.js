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


document.getElementById("login").addEventListener("click", login, false);
document.getElementById("api").addEventListener("click", api, false);
document.getElementById("logout").addEventListener("click", logout, false);

var config = {
    authority: "https://localhost:6004",
    client_id: "js",
    client_secret: "secret",
    redirect_uri: "https://localhost:6006/Home/Callback",
    response_type: "code",
    scope: "openid profile offline_access manage agg.stat",
    post_logout_redirect_uri: "https://localhost:6006/Home/Charts",
};

var mgr = new Oidc.UserManager(config);

mgr.getUser().then(function (user) {
    if (user) {
        console.log("User logged in");
    }
    else {
        console.log("User not logged in");
    }
});

function login() {
    mgr.signinRedirect();
}

function api() {
    mgr.getUser().then(function (user) {
        $.ajax({
            type: "GET",
            url: "https://localhost:6001/api/Statistics/getJobAppliedCount",
            dataType: 'json',
            beforeSend: function (xhr) {
                xhr.setRequestHeader("Authorization", "Bearer " + user.access_token)
            },
            success: function (data, status, xhr) {
                debugger;
                BuildBarChart('jobs-statistics-chart1', data["data"].jobsName, data["data"].applierCount, 'Top 5 Applied Jobs Bar Chart', 'Applier')
                BuildPieChart('jobs-statistics-chart2', data["data"].jobsName, data["data"].applierCount, 'Top 5 Applied Jobs Pie Chart', 'Applier')
            },
            error: function (jqXhr, textStatus, errorMessage) {
                debugger;
                alert("An error occured while loading the data");
            }
        });
    });
}

function logout() {
    mgr.signoutRedirect();
}