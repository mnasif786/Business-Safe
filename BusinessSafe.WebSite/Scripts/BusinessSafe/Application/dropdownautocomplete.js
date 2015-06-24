
$.widget("ui.combobox", {
    _create: function () {
        var _self = this, options = $.extend({}, this.options,
            {
                minLength: 0
            });

        if (_self.element.attr('readonly') === 'readonly' || _self.element.attr('disabled') === 'disabled') return;

        $(_self.element).bind("afterSelect", function () {
            if (_self.options.afterSelect !== undefined) {
                _self.options.afterSelect();
            }
        });

        var getHiddenFieldConventionName = function () {
            return $(_self.element).attr("id") + "Id";
        };

        var selectValue = function (dropdown, ui) {

            if (ui.item.label === "No Results") {
                _self.element.value = "";
                return false;
            }


            $(_self.element).val(ui.item.label);

            // CONVENTION TIME -- Looking for a field same name as the drop down with suffix id! 
            var hiddenField = $("#" + getHiddenFieldConventionName());
            if (hiddenField.length > 0) {
                hiddenField.val(ui.item.value);
            }

            if (options.afterSelect !== undefined) {
                options.afterSelect(dropdown, ui);
                //splice new typein values in 'initialValues List' if not already exists in that list     
                var newArr = $.grep(_self.options.initialValues, function (n, i) {
                    return (n.value == ui.item.value);
                });
                //newArr.length == 0 Means value is not already there, so add it
                if (newArr.length == 0 && _self.options.initialValues != undefined) {
                    _self.options.initialValues.splice(_self.options.initialValues.length + 1, 0, { label: ui.item.label, value: ui.item.value });
                    initialValuesCount = _self.options.initialValues.length;
                }
            }

            return false;
        };

        var successCallBack = function (result, response) {

            var noResultsLabel = "No Results";
            if (typeof result[0] === 'undefined') {
                result = [noResultsLabel];
                response(result);

                if (options.afterSearch !== undefined) {
                    options.afterSearch(result);
                }

                return;
            }

            response($.map(result, function (el, index) {
                return {
                    label: el.label,
                    value: el.value
                };
            }));
        };

        var source = function (request, response) {

            String.prototype.startsWith = function (str) {
                return (this.match("^" + str) == str);
            };

            var arr = jQuery.grep(_self.options.initialValues, function (n, i) {

                return (n.label.toLowerCase().startsWith(request.term.toLowerCase()));
            });

            if (arr.length > 0) {
                response(arr);
            }
            else if (!request.term.length) {
                response(_self.options.initialValues);
            } else {

                if (options.url !== "") {
                    var callBack = function (result) {
                        successCallBack(result, response);
                    };

                    options.data.filter = request.term;

                    AjaxCall.execute(options.url, callBack, options.data);
                }
            }

        };

        var focus = function (event, ui) {
            this.value = ui.item.label;
            event.preventDefault(); // Prevent the default focus behavior.
        };

        var _getFilterAttribute = function (item) {
            if (item.filterName !== undefined) {
                return ' data-' + item.filterName + '-filter="' + item.filterValue + '"';
            }
            return '';
        };

        // Setting the base autocomplete widget event functions to our overriding functions
        options.select = selectValue;
        options.source = source;
        options.focus = focus;

        this.element.autocomplete(options);

        var initialValuesCount = _self.options.initialValues !== undefined ? _self.options.initialValues.length : 0;

        this.element.attr("data-initial-count", initialValuesCount);

        this.element.on("focus", function () {

            if ($(this).val() === '--Select Option--') {
                $(this).val('');
            }
            //return false;
        });

        this.element.on("change", function () {
            var that = this;
            var resetOption = true;

            if (initialValuesCount > 0) {
                var arr = $.grep(_self.options.initialValues, function (n, i) {
                    
                    //escape values to handle parentheses in ddl
                    var arrayValue = escape(n.label.toLowerCase());
                    var compareValue = escape($(that).val().toLowerCase());
                    return (arrayValue.startsWith(compareValue));
                });
                if (arr.length > 0) {
                    $(that).val(arr[0].label);
                    $("#" + getHiddenFieldConventionName()).val(arr[0].value);

                    if (_self.options.afterSelect !== undefined) {
                        _self.options.afterSelect();
                    }
                    
                    resetOption = false;
                }
            }

            if (resetOption) {
                $(this).val('--Select Option--');
                $("#" + getHiddenFieldConventionName()).val('');
            }
        });

        this.button = $("<button type='button'><i class='icon-chevron-down'></i></button>").
            attr("tabIndex", -1).
            attr("title", "Show All Items").
            insertAfter(this.element).
            button({
                icons: {
                    primary: "ui-icon-triangle-1-s"
                },
                text: false
            }).
            removeClass("ui-corner-all").
            addClass("btn").
            click(function () {
                if (_self.element.autocomplete("widget").is(":visible")) {
                    _self.element.autocomplete("close");
                    return;
                }
                _self.element.autocomplete("search", "");
                _self.element.focus();
            });



        var hiddenField = $("#" + getHiddenFieldConventionName());
        $(_self.element).addClass(hiddenField.attr("class"));


        if (options.addDefaultOption === true) {
            _self.options.initialValues.splice(0, 0, { label: "--Select Option--", value: "" });
        }

        if (options.selectedId === undefined || options.selectedId === "" || options.selectedId === "0" || options.selectedId === "00000000-0000-0000-0000-000000000000") {
            $(_self.element).val(options.initialValues[0].label);
            hiddenField.val(options.initialValues[0].value);
        }
        else if (options.selectedId !== "") {
            var arr = jQuery.grep(_self.options.initialValues, function (n, i) {
                return n.value === options.selectedId;
            });
            if (arr.length > 0) {
                $(_self.element).val(arr[0].label);
            }
        }

    },
    RemoveItemFromInitialValues: function (newInitialValues) {
        this.options.initialValues = newInitialValues;
    }

});

