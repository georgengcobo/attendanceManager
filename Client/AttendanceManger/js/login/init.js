 $(document).ready(function() {
            $(".loader").hide();
            $(document).ajaxStart(function() {
                $(".loader").show();
            }).ajaxStop(function() {
                $(".loader").hide();
            });

	});
		