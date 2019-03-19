window.onload = function () {
    function processResponse(response) {
        if (response.isSuccessful) {
            return response.result;
        }
        var error = response.diagnostics[0];
        return makeArrow(error.span) + " " + getErrorMessage(error.kind, error.parameters);
    }

    function getErrorMessage(kind, params) {
        switch (kind) {
            case "UnexpectedToken":
                return "Expected: " + params[0] + ". But found: " + params[1];
            default:
                return "Error: " + kind;
        }
    }

    function makeArrow(span) {
        var res = "";
        for (var i = 0; i < span.start + 1; i++) {
            res = res + "-";
        }
        for (var j = 0; j < span.length; j++) {
            res = res + "^";
        }
        return res;
    }

    function showHelp(items) {
        items.push(new Item("Simple math expressions evaluator."));
        items.push(new Item("Input example: 2+3*4"));
        items.push(new Item("Supported operations: Binary: +-*/^ Unary: +- Parenthesis: ()"));
        items.push(new Item("REPL commands:"));
        items.push(new Item("#help - to show help"));
        items.push(new Item("#cls - clear screen"));
        items.push(new Item("#show-log - show log"));
        items.push(new Item("#download-log - to download log of all operations"));
    }

    function calculate(self, input) {
        var item = new Item("...");
        self.items.push(item);
        self.client.calculate(input,
            function (response) {
                item.val(processResponse(response));
                if (!response.isSuccessful) {
                    item.isError(true);

                }
            });
    }

    function showLog(self) {
        self.items([]);
        self.client.getLog(function(response) {
            for (var i = 0; i < response.length; i++) {
                var log = response[i];
                self.items.push(new Item(">" + log.input));
                var item = new Item(processResponse(log.output));
                if (!log.output.isSuccessful) {
                    item.isError(true);
                }
                self.items.push(item);
            }
        });
    }

    function processInput(self, input) {
        self.items.push(new Item(">" + input));
        switch (input.trim()) {
            case "#help":
                showHelp(self.items);
                break;
            case "#cls":
                self.items([]);
                break;
            case "#show-log":
                showLog(self);
                break;
            case "#download-log":
                self.client.downloadLog();
                break;
            default:
                calculate(self, input);
                break;
        }
        self.input("");
    }

    function Item(text) {
        this.val = ko.observable(text);
        this.isError = ko.observable(false);
    }

    function AppViewModel() {
        this.input = ko.observable("");
        this.items = ko.observableArray([new Item("Simple math expressions evaluator. For help type: #help")]).extend({ scrollFollow: '#container' });
        this.client = new ApiClient();
        this.calculate = function () {
            var self = this;
            processInput(self, self.input());
        };

        this.onEnterKey = function () {
            this.calculate();
        };
    }

    ko.applyBindings(new AppViewModel());

    $(window).focus(function () {
        $("#input").focus();
    });
    $("#input").focus();
};

