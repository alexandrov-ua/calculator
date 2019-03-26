import * as ko from "knockout"
import { ApiClient } from "./ApiClient"

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
    constructor(input, output, arrow, type) {
        this.input = ko.observable(input);
        this.output = ko.observable(output);
        this.arrow = ko.observable(arrow);
        this.type = ko.observable(type);
    }
}

class InfoLogItem {
    constructor(input, strs) {
        this.input = ko.observable(input);
        this.outputs = ko.observableArray(strs);
        this.type = ko.observable("info");
    }
}

class AppViewModel {
    constructor() {
        this.input = ko.observable("");
        this.logItems = ko.observableArray([new InfoLogItem("", ["For help type: #help"])]).extend({ scrollFollow: '#container' });
        this.client = new ApiClient();
        this.inputHistoryPointer = 0;
    }

    getLogType(logItem) {
        return logItem.type();
    }

    onEnterKey() {
        this.inputHistoryPointer = 0;
        let input = this.input();
        this.processInput(input);
    }

    onUpKey() {
        if (this.inputHistoryPointer < this.logItems().length - 1) {
            this.inputHistoryPointer += 1;
            this.input(this.logItems()[this.logItems().length - this.inputHistoryPointer].input());
        }
    }

    onDownKey() {
        if (this.inputHistoryPointer > 1) {
            this.inputHistoryPointer -= 1;
            this.input(this.logItems()[this.logItems().length - this.inputHistoryPointer].input());
        } else {
            this.inputHistoryPointer = 0;
            this.input("");
        }
    }

    processInput(input) {
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
                if (log.output.isSuccessful == true) {
                    this.logItems.push(new LogItem(log.input, log.output.result, "", "success"));
                } else {
                    var error = log.output.diagnostics[0];
                    this.logItems.push(new LogItem(log.input, getErrorMessage(error.kind, error.parameters), makeArrow(error.span), "error"));
                }
            }
        });
    }

    calculate(input) {
        var item = new LogItem(input, "...", "", "success");
        this.logItems.push(item);
        this.client.calculate(input,
            function (response) {
                if (response.isSuccessful) {
                    item.output(response.result);
                } else {
                    var error = response.diagnostics[0];
                    item.output(getErrorMessage(error.kind, error.parameters));
                    item.arrow(makeArrow(error.span));
                    item.type("error");
                }
            });
    }

    showHelp() {
        var msg = [
            "Simple math expressions evaluator.",
            "Input example: 2+3*4",
            "Supported operations: Binary: +-*/^ Unary: +- Parenthesis: ()",
            "REPL commands:",
            "#help - to show help",
            "#cls - clear screen",
            "#show-log - show log",
            "#download-log - to download log of all operations",
        ];
        this.logItems.push(new InfoLogItem(this.input(), msg));
    }
}

export { AppViewModel, LogItem as Item }