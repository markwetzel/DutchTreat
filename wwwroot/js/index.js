$(document).ready(function () {
	console.log("Hello");

	const theForm = $("#the-form");
	theForm.hide();

	const button = $("#buy-button");
	button.on("click",
		() => {
			console.log("Buying Item");
		});

	const productInfo = $(".product-props li");
	productInfo.on('click',
		function () {
			console.log("You clicked on " + $(this).text());
		});

	const $loginToggle = $('#login-toggle');
	const $popupForm = $('.popup-form');
	$loginToggle.on('click', function () {
		$popupForm.fadeToggle(1000);
	})
});