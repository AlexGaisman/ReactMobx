import { useState } from 'react';

export default function useToken() {
    const getToken = () => {
        const tokenString = sessionStorage.getItem('token');

        if (tokenString == null) return undefined;
        const userToken = JSON.stringify(tokenString);
        if (userToken == null) userToken = undefined;
        console.log("token="+ userToken);
        return userToken;
    };

    const [token, setToken] = useState(getToken());

    const saveToken = userToken => {
        sessionStorage.setItem('token', JSON.stringify(userToken));
        setToken(userToken);
    };

    const clearToken = () => {
        sessionStorage.removeItem('token');
    }

    return {
        setToken: saveToken,
        token,
        clearToken
    }


}