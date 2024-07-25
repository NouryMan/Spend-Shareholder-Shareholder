$(".visits-chart").sparkline( [0,0,1000,1250,3000,2500,2100,2500,2450,4000,2200,2300,2000,2100,1700,2020,2050,1800,1850,1100,1400,1750,1500], {
    type: 'line',
    width: '90px',
    height: '25px',
    lineColor: '#287dfa',
    fillColor: '#ebf5f9',
    spotColor: '#2273e5',
    minSpotColor: '#2273e5',
    maxSpotColor: '#2273e5',
    highlightSpotColor: '#808996',
    highlightLineColor: '#808996',
    spotRadius: 0,
    chartRangeMin: 5,
    chartRangeMax: 10,
    chartRangeMinX: 5,
    chartRangeMaxX: 5,
    normalRangeMin: 5,
    normalRangeMax: 5,
    normalRangeColor: '#ebf5f9',
    drawNormalOnTop: true

});


$(".previews-chart").sparkline([1,21,17,20,50,18,16,1,5,20,14,12,11,25,7,3,35,23,16,12,7,16,25], {
    type: 'line',
    width: '90px',
    height: '25px',
    lineColor: '#287dfa',
    fillColor: '#ebf5f9',
    spotColor: '#2273e5',
    minSpotColor: '#2273e5',
    maxSpotColor: '#2273e5',
    highlightSpotColor: '#808996',
    highlightLineColor: '#808996',
    spotRadius: 0,
    chartRangeMin: 5,
    chartRangeMax: 10,
    chartRangeMinX: 5,
    chartRangeMaxX: 5,
    normalRangeMin: 5,
    normalRangeMax: 5,
    normalRangeColor: '#ebf5f9',
    drawNormalOnTop: true
});

$(".visits-chart-2").sparkline([1,3,5,10,8,9,10,8,7,8,9,7,8,7,9,8,7,8,7,8,9,10,8,9,8,10,6], {
    type: 'line',
    width: '90px',
    height: '15px',
    lineColor: '#287dfa',
    fillColor: '#ebf5f9',
    spotColor: '#2273e5',
    minSpotColor: '#2273e5',
    maxSpotColor: '#2273e5',
    highlightSpotColor: '#808996',
    highlightLineColor: '#808996',
    spotRadius: 0,
    chartRangeMin: 5,
    chartRangeMax: 10,
    chartRangeMinX: 5,
    chartRangeMaxX: 5,
    normalRangeMin: 5,
    normalRangeMax: 5,
    normalRangeColor: '#ebf5f9',
    drawNormalOnTop: true
});


//new dashbord

/*
Template Name: Admin Pro Admin
Author: Wrappixel
Email: niravjoshi87@gmail.com
File: js
*/
$(function () {
    "use strict";

    // ============================================================== 
    // Sales overview
    // ============================================================== 
    var chart2 = new Chartist.Bar('.amp-pxl', {
        labels: ['Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat', 'Sun'],
        series: [
            [9, 5, 3, 7, 5, 10, 3],
            [6, 3, 9, 5, 4, 6, 4]
        ]
    }, {
        axisX: {
            // On the x-axis start means top and end means bottom
            position: 'end',
            showGrid: false
        },
        axisY: {
            // On the y-axis start means left and end means right
            position: 'start'
        },
        high: '12',
        low: '0',
        plugins: [
            Chartist.plugins.tooltip()
        ]
    });

    var chart = [chart2];

    // ============================================================== 
    // This is for the animation
    // ==============================================================

    for (var i = 0; i < chart.length; i++) {
        chart[i].on('draw', function (data) {
            if (data.type === 'line' || data.type === 'area') {
                data.element.animate({
                    d: {
                        begin: 500 * data.index,
                        dur: 500,
                        from: data.path.clone().scale(1, 0).translate(0, data.chartRect.height()).stringify(),
                        to: data.path.clone().stringify(),
                        easing: Chartist.Svg.Easing.easeInOutElastic
                    }
                });
            }
            if (data.type === 'bar') {
                data.element.animate({
                    y2: {
                        dur: 500,
                        from: data.y1,
                        to: data.y2,
                        easing: Chartist.Svg.Easing.easeInOutElastic
                    },
                    opacity: {
                        dur: 500,
                        from: 0,
                        to: 1,
                        easing: Chartist.Svg.Easing.easeInOutElastic
                    }
                });
            }
        });
    }

    // ============================================================== 
    // Our visitor
    // ============================================================== 

    var chart = c3.generate({
        bindto: '#visitor',
        data: {
            columns: [
                ['Other', 30],
                ['Desktop', 10],
                ['Tablet', 40],
                ['Mobile', 50],
            ],

            type: 'donut',
            onclick: function (d, i) { console.log("onclick", d, i); },
            onmouseover: function (d, i) { console.log("onmouseover", d, i); },
            onmouseout: function (d, i) { console.log("onmouseout", d, i); }
        },
        donut: {
            label: {
                show: false
            },
            title: "Our visitor",
            width: 20,

        },

        legend: {
            hide: true
            //or hide: 'data1'
            //or hide: ['data1', 'data2']
        },
        color: {
            pattern: ['#eceff1', '#745af2', '#26c6da', '#1e88e5']
        }
    });

});