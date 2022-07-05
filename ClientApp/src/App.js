import React, { lazy, Component, useState, Suspense, useContext } from 'react';
import {Route, Switch, Redirect } from 'react-router-dom';
/*import { Route } from 'react-router';*/
import { Layout } from './components/Layout';
import { FetchData } from './components/FetchData';
import { Counter } from './components/Counter';
import { Signup } from './pages/Signup'
import { UserLogin } from './auth/Login'
import useToken from './auth/useToken'

import './App.css';
import { AuthProvider, AuthContext } from './context/AuthContext';
import { FetchProvider } from './context/FetchContext';
import AppShell from './AppShell';



import FourOFour from './pages/FourOFour';
import Login from './pages/Login'
import Home from './pages/Home';
import PrintServices from './pages/PrintServices';
import Dashboard from './pages/Dashboard';
import Inventory from './pages/Inventory';
import Account from './pages/Account';
import Settings from './pages/Settings';
import Users from './pages/Users';

import * as stores from './stores';
import { Provider, observer } from 'mobx-react';

const LoadingFallback = () => (
    <AppShell>
        <div className="p-4">Loading...</div>
    </AppShell>
);

const UnauthenticatedRoutes = () => (
    <Switch>
        <Route path="/login">
            <Login />
        </Route>
        <Route path="/signup">
            <Signup />
        </Route>
        <Route exact path="/">
            <Home />
        </Route>
        <Route exact path="/PrintServices">
            <PrintServices />
        </Route>
        <Route path="*">
            <FourOFour />
        </Route>
    </Switch>
);


const AdminRoute = ({ children, ...rest }) => {
    const auth = useContext(AuthContext);
    return (
        <Route
            {...rest}
            render={() =>
                auth.isAuthenticated() && auth.isAdmin() ? (
                    <AppShell>{children}</AppShell>
                ) : (
                    <Redirect to="/" />
                )
            }
        ></Route>
    );
};

const AuthenticatedRoute = ({ children, ...rest }) => {
    const auth = useContext(AuthContext);
    return (
        <Route
            {...rest}
            render={() =>
                auth.isAuthenticated() ? (
                    <AppShell>{children}</AppShell>
                ) : (
                    <Redirect to="/" />
                )
            }
        ></Route>
    );
};

const AppRoutes = () => {
    return (
        <>
            <Provider {...stores }>
                <Suspense fallback={<LoadingFallback />}>
                    <Switch>
                        <AuthenticatedRoute path="/dashboard">
                            <Dashboard />
                        </AuthenticatedRoute>
                        <AdminRoute path="/inventory">
                            <Inventory />
                        </AdminRoute>
                        <AuthenticatedRoute path="/account">
                            <Account />
                        </AuthenticatedRoute>
                        <AuthenticatedRoute path="/settings">
                            <Settings />
                        </AuthenticatedRoute>
                        <AuthenticatedRoute path="/users">
                            <Users />
                        </AuthenticatedRoute>
                        <UnauthenticatedRoutes />
                    </Switch>
                </Suspense>
            </Provider>
        </>
    );
};

export  const App = (props) => {

    //const { token, setToken } = useToken();

    //if (!token) {
    //    return <UserLogin setToken={setToken} />
    //}

    return (
        <AuthProvider>
            <FetchProvider>
                <div className="bg-gray-100">
                    <AppRoutes />
                </div>
                {/*<Layout>*/}
                {/*    <Route exact path='/' component={Home} />*/}
                {/*    <Route path='/counter' component={Counter} />*/}
                {/*    <Route path='/fetch-data' component={FetchData} />*/}
                {/*    <Route path='/register' component={Signup} />*/}
                {/*    <Route path='/userProfile' component={Signup} />*/}
                {/*    <Route path='/login' render={(props) => (<UserLogin {...props} setToken={setToken} />)} />*/}
                {/*</Layout>*/}
            </FetchProvider>
        </AuthProvider>
    );
}

export default App;

