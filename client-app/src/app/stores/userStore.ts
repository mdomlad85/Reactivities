import {makeAutoObservable, runInAction} from "mobx";
import agent from "../api/agent";
import {User, UserFormValues} from "../models/user";
import {history} from "../../index";
import {store} from "./store";
import {Token} from "../common/constants/token";

export default class UserStore {
    user: User | null = null;

    constructor() {
        makeAutoObservable(this);
    }

    get isLoggedIn() {
        return !!this.user;
    }

    login = async (creds: UserFormValues) => {
        try {
            const user = await agent.Account.login(creds);
            store.commonStore.setToken(user.token);
            runInAction(() => this.user = user);
            history.push('/activities');
            store.modalStore.closeModal();
        } catch (error) {
            throw error;
        }
    }

    logout = () => {
        store.commonStore.setToken(null);
        window.localStorage.removeItem(Token.Name);
        runInAction(() => this.user = null);
        history.push('/');
    }

    getUser = async () => {
        try {
            const user = await agent.Account.current();
            runInAction(() => this.user = user);
        } catch (error) {
            console.log(error);
        }
    }

    register = async (userInfo: UserFormValues) => {
        try {
            const user = await agent.Account.register(userInfo);
            store.commonStore.setToken(user.token);
            runInAction(() => this.user = user);
            history.push('/activities');
            store.modalStore.closeModal();
        } catch (error) {
            throw error;
        }
    }

    setImage = (image: string) => {
        if (this.user) this.user.image = image;
    }

    setDisplayName = (displayName: string) => {
        if (this.user) this.user.displayName = displayName;
    }
}