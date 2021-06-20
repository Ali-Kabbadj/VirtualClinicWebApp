function ajaxCall() {
    this.send = function (data, url, method, success, type) {
        type = type || 'json';
        var successRes = function (data) {
            success(data);
        }

        var errorRes = function (e) {
            console.log(e);
        }
        $.ajax({
            url: url,
            type: method,
            data: data,
            success: successRes,
            error: errorRes,
            dataType: type,
            timeout: 60000
        });

    }

}

function locationInfo() {
    var rootUrl = "https://geodata.solutions/api/api.php";
    //now check for set values
    var addParams = '';
    if ($("#gds_appid").length > 0) {
        addParams += '&appid=' + $("#gds_appid").val();
    }
    if ($("#gds_hash").length > 0) {
        addParams += '&hash=' + $("#gds_hash").val();
    }

    var call = new ajaxCall();

    this.confCity = function (id) {
        var url = rootUrl + '?type=confCity&countryId=' + $('#countryId option:selected').attr('countryid') + '&stateId=' + $('#stateId option:selected').attr('stateid') + '&cityId=' + id;
        var method = "post";
        var data = {};
        call.send(data, url, method, function (data) {
        });
    };


    this.getCities = function (id) {
        $(".cities option:gt(0)").remove();
        var stateClasses = $('#cityId').attr('class');

        var cC = stateClasses.split(" ");
        cC.shift();
        var addClasses = '';
        if (cC.length > 0) {
            acC = cC.join();
            addClasses = '&addClasses=' + encodeURIComponent(acC);
        }
        var url = rootUrl + '?type=getCities&countryId=' + $('#countryId option:selected').attr('countryid') + '&stateId=' + id + addParams + addClasses;
        var method = "post";
        var data = {};
        $('.cities').find("option:eq(0)").html("Please wait..");
        call.send(data, url, method, function (data) {
            $('.cities').find("option:eq(0)").html("Select City");
            if (data.tp == 1) {
                var listlen = Object.keys(data['result']).length;

                if (listlen > 0) {
                    $.each(data['result'], function (key, val) {

                        var option = $('<option />');
                        option.attr('value', val).text(val);
                        $('.cities').append(option);
                    });
                }
                else {
                    var usestate = $('#stateId option:selected').val();
                    var option = $('<option />');
                    option.attr('value', usestate).text(usestate);
                    option.attr('selected', 'selected');
                    $('.cities').append(option);
                }

                $(".cities").prop("disabled", false);
            }
            else {
                alert(data.msg);
            }
        });
    };

    this.getStates = function (id) {
        $(".states option:gt(0)").remove();
        $(".cities option:gt(0)").remove();
        //get additional fields
        var stateClasses = $('#stateId').attr('class');

        var cC = stateClasses.split(" ");
        cC.shift();
        var addClasses = '';
        if (cC.length > 0) {
            acC = cC.join();
            addClasses = '&addClasses=' + encodeURIComponent(acC);
        }
        var url = rootUrl + '?type=getStates&countryId=' + id + addParams + addClasses;
        var method = "post";
        var data = {};
        $('.states').find("option:eq(0)").html("Please wait..");
        call.send(data, url, method, function (data) {
            $('.states').find("option:eq(0)").html("Select State");
            if (data.tp == 1) {
                $.each(data['result'], function (key, val) {
                    var option = $('<option />');
                    option.attr('value', val).text(val);
                    option.attr('stateid', key);
                    $('.states').append(option);
                });
                $(".states").prop("disabled", false);
            }
            else {
                alert(data.msg);
            }
        });
    };

    this.getCountries = function () {
        //get additional fields
        var countryClasses = $('#countryId').attr('class');

        var cC = countryClasses.split(" ");
        cC.shift();
        var addClasses = '';
        if (cC.length > 0) {
            acC = cC.join();
            addClasses = '&addClasses=' + encodeURIComponent(acC);
        }

        var presel = false;
        var iip = 'N';
        $.each(cC, function (index, value) {
            if (value.match("^presel-")) {
                presel = value.substring(7);

            }
            if (value.match("^presel-byi")) {
                var iip = 'Y';
            }
        });


        var url = rootUrl + '?type=getCountries' + addParams + addClasses;
        var method = "post";
        var data = {};
        $('.countries').find("option:eq(0)").html("Please wait..");
        call.send(data, url, method, function (data) {
            $('.countries').find("option:eq(0)").html("Select Country");

            if (data.tp == 1) {
                if (presel == 'byip') {
                    presel = data['presel'];
                    console.log('2 presel is set as ' + presel);
                }


                if ($.inArray("group-continents", cC) > -1) {
                    var $select = $('.countries');
                    console.log(data['result']);
                    $.each(data['result'], function (i, optgroups) {
                        var $optgroup = $("<optgroup>", { label: i });
                        if (optgroups.length > 0) {
                            $optgroup.appendTo($select);
                        }

                        $.each(optgroups, function (groupName, options) {
                            var coption = $('<option />');
                            coption.attr('value', options.name).text(options.name);
                            coption.attr('countryid', options.id);
                            if (presel) {
                                if (presel.toUpperCase() == options.id) {
                                    coption.attr('selected', 'selected');
                                }
                            }
                            coption.appendTo($optgroup);
                        });
                    });
                }
                else {
                    $.each(data['result'], function (key, val) {
                        var option = $('<option />');
                        option.attr('value', val).text(val);
                        option.attr('countryid', key);
                        if (presel) {
                            if (presel.toUpperCase() == key) {
                                option.attr('selected', 'selected');
                            }
                        }
                        $('.countries').append(option);
                    });
                }
                if (presel) {
                    $('.countries').trigger('change');
                }
                $(".countries").prop("disabled", false);
            }
            else {
                alert(data.msg);
            }
        });
    };

}

$(function () {
    var loc = new locationInfo();
    loc.getCountries();
    $(".countries").on("change", function (ev) {
        var countryId = $("option:selected", this).attr('countryid');
        if (countryId != '') {
            loc.getStates(countryId);
        }
        else {
            $(".states option:gt(0)").remove();
        }
    });
    $(".states").on("change", function (ev) {
        var stateId = $("option:selected", this).attr('stateid');
        if (stateId != '') {
            loc.getCities(stateId);
        }
        else {
            $(".cities option:gt(0)").remove();
        }
    });

    $(".cities").on("change", function (ev) {
        var cityId = $("option:selected", this).val();
        if (cityId != '') {
            loc.confCity(cityId);
        }
    });
});