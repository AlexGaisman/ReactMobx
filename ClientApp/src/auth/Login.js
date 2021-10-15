import React, { useState, Redirect } from 'react';
import PropTypes from 'prop-types';
import './Auth.css';
import { useHistory } from "react-router-dom";

export const UserLogin = ({ setToken}) => {
    const [password, setPassword] = useState('');
    const [email, setEmail] = useState('');
    const [redirect, setRedirect] = useState(false);

    async function loginUser(data) {
        return fetch('api/Auth/login', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(data)
        })
            .then(data => data.json())
            .then(data => {
                console.log(data);
                return data.token;
            });
        
    }

    const HandleSubmit = async e => {
        e.preventDefault();

        const token = await loginUser({
            password,
            email
        });

        setToken(token);

        setRedirect(true);
    }
    if (redirect)
        return <Redirect to="/" />;

    return (
        <div className="login-wrapper" >
            <h1> Please Log In </h1>
            <form onSubmit={HandleSubmit}>
                <label>
                    <p>Email</p>
                    <input type="email" onChange={e => setEmail(e.target.value)} />
                </label>
                <label>
                    <p>Password</p>
                    <input type="password" onChange={e => setPassword(e.target.value)}/>
                </label>
                <div>
                    <button type="submit"> Submit</button>
                </div>
            </form>
        </div>
    );
};

UserLogin.propTypes = {
    setToken: PropTypes.func.isRequired
};

export default UserLogin;
