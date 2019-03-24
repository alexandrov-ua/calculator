import * as ko from "knockout"
import { ApiClient } from "./ApiClient"

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

class LogItem {
    constructor(text) {
        this.val = ko.observable(text);
        this.isError = ko.observable(false);
    }
}

class AppViewModel {
    constructor() {
        this.input = ko.observable("");
        this.logItems = ko.observableArray([new LogItem("Simple math expressions evaluator. For help type: #help")]).extend({ scrollFollow: '#container' });
        this.client = new ApiClient();
    }

    onEnterKey() {
        let input = this.input();
        this.processInput(input);
    }

    processInput(input) {
        this.logItems.push(new LogItem(">" + input));
        switch (input.trim()) {
            case "#help":
                this.showHelp();
                break;
            case "#cls":
                this.logItems([]);
                break;
            case "#show-log":
                this.showLog();
                break;
            case "#download-log":
                this.client.downloadLog();
                break;
            default:
                this.calculate(input);
                break;
        }
        this.input("");
    }

    showLog() {
        this.logItems([]);
        this.client.getLog((response) => {
            for (var i = 0; i < response.length; i++) {
                var log = response[i];
                this.logItems.push(new LogItem(">" + log.input));
                var item = new LogItem(processResponse(log.output));
                if (!log.output.isSuccessful) {
                    item.isError(true);
                }
                this.logItems.push(item);
            }
        });
    }

    calculate(input) {
        var item = new LogItem("...");
        this.logItems.push(item);
        this.client.calculate(input,
            function (response) {
                item.val(processResponse(response));
                if (!response.isSuccessful) {
                    item.isError(true);
                }
            });
    }

    showHelp() {
        this.logItems.push(new LogItem("Simple math expressions evaluator."));
        this.logItems.push(new LogItem("Input example: 2+3*4"));
        this.logItems.push(new LogItem("Supported operations: Binary: +-*/^ Unary: +- Parenthesis: ()"));
        this.logItems.push(new LogItem("REPL commands:"));
        this.logItems.push(new LogItem("#help - to show help"));
        this.logItems.push(new LogItem("#cls - clear screen"));
        this.logItems.push(new LogItem("#show-log - show log"));
        this.logItems.push(new LogItem("#download-log - to download log of all operations"));
    }
}

export { AppViewModel, LogItem as Item }