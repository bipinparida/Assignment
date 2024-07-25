// Europe
$(function(){
	$('#mapEurope').vectorMap({
		map: 'europe_mill',
		zoomOnScroll: false,
		series: {
			regions: [{
				values: gdpData,
				scale: ['#239d2c', '#e5e8f2'],
				normalizeFunction: 'polynomial'
			}]
		},
		backgroundColor: '#ffffff',
	});
});