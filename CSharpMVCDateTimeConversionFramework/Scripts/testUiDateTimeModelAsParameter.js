(function () {

    $(document).ready(function () {

        $('.datepicker').datepicker({
            buttonImage: '/Content/themes/base/images/calendar.png',
            buttonImageOnly: true,
            buttonText: 'choose a date...',
            showOn: 'both',
            changeMonth: true,
            changeYear: true,
            constrainInput: true,
            minDate: new Date(),
            dateFormat: 'mm/dd/yy'
        });


        $('#doAjaxPost').on('click', function () {
            var timeZone1 = $('#ModelOne_TimeZoneName').val();
            var localDate1 = $('#ModelOne_LocalDate').val();
            var timeZone2 = $('#ModelTwo_TimeZoneName').val();
            var localDate2 = $('#ModelTwo_LocalDate').val();
            
            //********** Left here as a reference **********
            //                    var data = {
            //                        model: { TimeZoneName: $('#Model_TimeZoneName').val(), DateTimeLocalValue: $('#Model_LocalDate').val() },
            //                        model2: { TimeZoneName: $('#Model2_TimeZoneName').val(), DateTimeLocalValue: $('#Model2_LocalDate').val() },
            //                        anotherProperty: 'some string'
            //                    };


            if (timeZone1.length && localDate1.length && timeZone2.length && localDate2.length) {
                var data = [{ name: "modelOne.TimeZoneName", value: timeZone1 },
                    { name: "modelOne.DateTimeLocalValue", value: localDate1 },
                    { name: "modelTwo.TimeZoneName", value: timeZone2 },
                    { name: "modelTwo.DateTimeLocalValue", value: localDate2 },
                    { name: 'anotherProperty', value: 'some string' }
                ];

                $.ajax({
                    type: "POST",
                    url: $('form').attr('action'),
                    data: data
                }).done(function(response) {
                    console.log(response);
                    $('div.results').css("font-weight", "bold").css("color", "blue").html(JSON.stringify(response)); 
                });
            }else {
                $('div.results').css("font-weight", "bold").css("color", "red").html('Please Enter Date Values for all Inputs');
            }

        });

        $('#doAjaxGet').on('click', function () {
            var timeZone1 = $('#ModelOne_TimeZoneName').val();
            var localDate1 = $('#ModelOne_LocalDate').val();
            var timeZone2 = $('#ModelTwo_TimeZoneName').val();
            var localDate2 = $('#ModelTwo_LocalDate').val();
            
            //********** Left here as a reference **********
            //            var data = {
            //                        model: { TimeZoneName: $('#Model_TimeZoneName').val(), DateTimeLocalValue: $('#Model_LocalDate').val() },
            //                        model2: { TimeZoneName: $('#Model2_TimeZoneName').val(), DateTimeLocalValue: $('#Model2_LocalDate').val() },
            //                        anotherProperty: 'some string'
            //                    };

            if (timeZone1.length && localDate1.length && timeZone2.length && localDate2.length) {
                var data = [{ name: "modelOne.TimeZoneName", value: timeZone1 },
                                    { name: "modelOne.DateTimeLocalValue", value: localDate1 },
                                    { name: "modelTwo.TimeZoneName", value: timeZone2 },
                                    { name: "modelTwo.DateTimeLocalValue", value: localDate2 },
                                    { name: 'anotherProperty', value: 'some string' }
                                ];

                $.ajax({
                    type: "GET",
                    url: $('form').attr('action').replace('TestUiDateTimeModelAsParameter', 'TestUiDateTimeModelAsParameterWithGet'),
                    data: data
                }).done(function (response) {
                    console.log(response);
                    $('div.results').css("font-weight", "bold").css("color", "blue").html(JSON.stringify(response)); 
                });
            } else {
                $('div.results').css("font-weight", "bold").css("color", "red").html('Please Enter Date Values for all Inputs');
            }                

        });


    });
} ());