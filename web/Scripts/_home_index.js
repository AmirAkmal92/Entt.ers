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
        .delay(function (d, i) { return i * 75; })
        .duration(100)
        .attrTween("d", function (d) {
            var interpolate = d3.interpolate(d.startAngle, d.endAngle);
            return function (t) {
                d.endAngle = interpolate(t);
                return arc(d);
            };
        });
}  