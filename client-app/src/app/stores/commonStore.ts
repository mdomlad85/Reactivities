import {ServerError} from "../models/serverError";
import {makeAutoObservable, reaction} from "mobx";
import {Token} from "../common/constants/token";

export default class CommonStore {
    error: ServerError | null = null;
    token: string | null = window.localStorage.getItem(Token.Name);
    appLoaded = false;

    constructor() {
        makeAutoObservable(this);

        reaction(
            () => this.token,
            token => {
                if (token) {
                    window.localStorage.setItem(Token.Name, token);
                } else {
                    window.localStorage.removeItem(Token.Name);
                }
            }
        )
    }

    setServerError = (error: ServerError) => {
        this.error = error;
    }

    setToken = (token: string | null) => {
        this.token = token;
    }

    setAppLoaded = () => {
        this.appLoaded = true;
    }
}