$(document).ready(function () {


	var $body = $('body'),
		$content = $('#content');


	//IE doen't like that fadein
	if (!$.browser.msie) $body.fadeTo(0, 0.0).delay(500).fadeTo(0, 1);


	//$("input").not('input[type=submit]').uniform();

	$content.find('.breadcrumb1').wl_Breadcrumb({
		allownextonly: false,
		disabled: false,
		onChange: function (el, id) {
			switch (id) {
				case 0: //Primary Registration
					SetFocusOnStep1();
					break;
				case 1: //Additional Information
					SetFocusOnStep2();
					break;
				case 2: //Stewardships
					break;
				case 3: //Stakeholders
					break;
				case 4: // Suppliers
					SetFocusOnStep4();
					break;
				case 5: // Clients
					SetFocusOnStep5();
					break;
				case 6: // Login Confirmation
					SetFocusOnStep6();
					break;
				case 7: // Submit Application
					break;
			}
		}
	});

	$('#loginbtn').click(function () {
		location.href = "login.html";
	});


});
