// NAVBAR ADD SHADOW AFTER SCROLL
$(function () {
    $(document).scroll(function () {
        var $nav = $(".navbar");
        $nav.toggleClass('scrolled', $(this).scrollTop() > $nav.height());
    });
});

// SCROLL TO TOP
window.onscroll = function () {
    scrollFunction()
};

function scrollFunction() {
    document.getElementById("backTop").style.display = 20 < document.body.scrollTop || 20 < document.documentElement.scrollTop ? "block" : "none"
}

function topFunction() {
    document.body.scrollTop = 0, document.documentElement.scrollTop = 0
}


var ctx = document.getElementById('myChart').getContext('2d');
var myChart = new Chart(ctx, {
    type: 'line',
    data: {
        labels: ['Red', 'Blue', 'Yellow', 'Green', 'Purple', 'Orange'],
        datasets: [{
            label: '# of Votes',
            data: [12, 19, 3, 5, 2, 3],
            backgroundColor: [
                'rgba(234, 235, 255, 1)',
                'rgba(234, 235, 255, 1)',
                'rgba(234, 235, 255, 1)',
                'rgba(234, 235, 255, 1)',
                'rgba(234, 235, 255, 1)',
                'rgba(234, 235, 255, 1)'
            ],
            borderColor: [
                'rgba(0, 9, 202, 1)',
                'rgba(0, 9, 202, 1)',
                'rgba(0, 9, 202, 1)',
                'rgba(0, 9, 202, 1)',
                'rgba(0, 9, 202, 1)',
                'rgba(0, 9, 202, 1)',
            ],
            borderWidth: 1
        }]
    },
    options: {
        scales: {
            yAxes: [{
                ticks: {
                    beginAtZero: true
                }
            }]
        }
    }
});