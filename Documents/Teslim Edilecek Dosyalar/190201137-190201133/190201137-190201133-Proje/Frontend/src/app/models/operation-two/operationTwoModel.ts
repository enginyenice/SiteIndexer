import { Keyword } from "../keyword/keywordModel";

export interface OperationTwoModel{
    url:string;
    title:string;
    keywords:Keyword[];
}