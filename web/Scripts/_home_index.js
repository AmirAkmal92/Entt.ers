function acceptanceDonut(element, size, data) {
    var d3Container = d3.select(element),
        distance = 2, // reserve 2px space for mouseover arc moving
        radius = (size / 2) - distance,
        sum = d3.sum(data, function (d) { return d.value; })

    var tip = d3.tip()
        .attr('class', 'd3-tip')
        .offset([-10, 0])
        .direction('e')
        .html(function (d) {
            return "<ul class='list-unstyled mb-5'>" +
                "<li>" + "<div class='text-size-base mb-5 mt-5'>" + d.data.icon + d.data.browser + "</div>" + "</li>" +
                "<li>" + "Visits: &nbsp;" + "<span class='text-semibold pull-right'>" + d.value + "</span>" + "</li>" +
                "<li>" + "Share: &nbsp;" + "<span class='text-semibold pull-right'>" + (100 / (sum / d.value)).toFixed(2) + "%" + "</span>" + "</li>" +
                "</ul>";
        })

    var container = d3Container.append("svg").call(tip);

    var svg = container
        .attr("width", size)
        .attr("height", size)
        .append("g")
        .attr("transform", "translate(" + (size / 2) + "," + (size / 2) + ")");

    var pie = d3.layout.pie()
        .sort(null)
        .startAngle(Math.PI)
        .endAngle(3 * Math.PI)
        .value(function (d) {
            return d.value;
        });

    var arc = d3.svg.arc()
        .outerRadius(radius)
        .innerRadius(radius / 2);

    var arcGroup = svg.selectAll(".d3-arc")
        .data(pie(data))
        .enter()
        .append("g")
        .attr("class", "d3-arc")
        .style('stroke', '#fff')
        .style('cursor', 'pointer');

    var arcPath = arcGroup
        .append("path")
        .style("fill", function (d) { return d.data.color; });

    arcPath
        .on('mouseover', function (d, i) {

            // Transition on mouseover
            d3.select(this)
                .transition()
                .duration(500)
                .ease('elastic')
                .attr('transform', function (d) {
                    d.midAngle = ((d.endAngle - d.startAngle) / 2) + d.startAngle;
                    var x = Math.sin(d.midAngle) * distance;
                    var y = -Math.cos(d.midAngle) * distance;
                    return 'translate(' + x + ',' + y + ')';
                });
        })

        .on("mousemove", function (d) {

            // Show tooltip on mousemove
            tip.show(d)
                .style("top", (d3.event.pageY - 40) + "px")
                .style("left", (d3.event.pageX + 30) + "px");
        })

        .on('mouseout', function (d, i) {

            // Mouseout transition
            d3.select(this)
                .transition()
                .duration(500)
                .ease('bounce')
                .attr('transform', 'translate(0,0)');

            // Hide tooltip
            tip.hide(d);
        });

    // Animate chart on load
    arcPath
        .transition()
        .delay(function (d, i) { return i * 150; })
        .duration(100)
        .attrTween("d", function (d) {
            var interpolate = d3.interpolate(d.startAngle, d.endAngle);
            return function (t) {
                d.endAngle = interpolate(t);
                return arc(d);
            };
        });
}  

function generateBarChart(element, data, height, animate, easing, duration, delay, color, tooltip) {

    // Add data set
    var bardata = [];
    $.each(data, function (i, d) {
        bardata.push(d);
    });

    //for (var i = 0; i < barQty; i++) {
    //    bardata.push(Math.round(Math.random() * 10) + 10)
    //}

    // Main variables
    var d3Container = d3.select(element),
        width = d3Container.node().getBoundingClientRect().width;

    // Horizontal
    var x = d3.scale.ordinal()
        .rangeBands([0, width], 0.3)

    // Vertical
    var y = d3.scale.linear()
        .range([0, height]);

    // Horizontal
    x.domain(d3.range(0, bardata.length))

    // Vertical
    y.domain([0, d3.max(bardata)])

    //var xAxis = d3.svg.axis()
    //    .scale(x)
    //    .orient("bottom");

    // Add svg element
    var container = d3Container.append('svg');

    // Add SVG group
    var svg = container
        .attr('width', width)
        .attr('height', height)
        .append('g');

    //svg.append("g")
    //    .attr("class", "d3-axis d3-axis-horizontal d3-axis-strong")
    //    .attr("transform", "translate(0," + height + ")")
    //    .call(xAxis);

    // Bars
    var bars = svg.selectAll('rect')
        .data(bardata)
        .enter()
        .append('rect')
        .attr('class', 'd3-random-bars')
        .attr('width', x.rangeBand())
        .attr('x', function (d, i) {
            return x(i);
        })
        .style('fill', color);

    var tip = d3.tip()
        .attr('class', 'd3-tip')
        .offset([-10, 0]);

    // Show and hide
    if (tooltip == "hours" || tooltip == "goal" || tooltip == "items") {
        bars.call(tip)
            .on('mouseover', tip.show)
            .on('mouseout', tip.hide);
    }

    // Online members tooltip content
    if (tooltip == "items") {
        tip.html(function (d, i) {
            return "<div class='text-center'>" +
                "<h6 class='no-margin'>" + d + "</h6>" +
                "<span class='text-size-small'>items</span>" +
                "<div class='text-size-small'>" + i + ":00" + "</div>" +
                "</div>"
        });
    }
    
    // Choose between animated or static
    if (animate) {
        withAnimation();
    } else {
        withoutAnimation();
    }

    // Animate on load
    function withAnimation() {
        bars
            .attr('height', 0)
            .attr('y', height)
            .transition()
            .attr('height', function (d) {
                return y(d);
            })
            .attr('y', function (d) {
                return height - y(d);
            })
            .delay(function (d, i) {
                return i * delay;
            })
            .duration(duration)
            .ease(easing);
    }

    // Load without animateion
    function withoutAnimation() {
        bars
            .attr('height', function (d) {
                return y(d);
            })
            .attr('y', function (d) {
                return height - y(d);
            })
    }

    // Call function on window resize
    $(window).on('resize', barsResize);

    // Call function on sidebar width change
    $(document).on('click', '.sidebar-control', barsResize);

    // Resize function
    // 
    // Since D3 doesn't support SVG resize by default,
    // we need to manually specify parts of the graph that need to 
    // be updated on window resize
    function barsResize() {

        // Layout variables
        width = d3Container.node().getBoundingClientRect().width;


        // Layout
        // -------------------------

        // Main svg width
        container.attr("width", width);

        // Width of appended group
        svg.attr("width", width);

        // Horizontal range
        x.rangeBands([0, width], 0.3);

        //svg.selectAll('.d3-axis-horizontal').call(xAxis);

        // Chart elements
        // -------------------------

        // Bars
        svg.selectAll('.d3-random-bars')
            .attr('width', x.rangeBand())
            .attr('x', function (d, i) {
                return x(i);
            });
    }
}