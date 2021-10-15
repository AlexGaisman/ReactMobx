import React, { useState, Redirect } from 'react';
import {
    Collapse, Container, Navbar, NavbarBrand,
    NavbarToggler, NavItem, NavLink, NavbarText
} from 'reactstrap';
import { Link, useHistory } from 'react-router-dom';
//import './NavMenu.css';
import useToken from '../auth/useToken'
import { Alert,Jumbotron } from 'reactstrap';

export const NavUser = (props) => {

    const Logout = (event) => {
        event.preventDefault();
        var ok = window.confirm('Do you really want to logout');
        if (ok) {
            clearToken();
            setRedirect(true);
        }
}

    const { token, clearToken } = useToken();
    const [redirect, setRedirect] = useState(false);

    if (redirect)
        return <Redirect to="/" />;

    return (
        <header className="d-sm-inline-flex" >
            {/*<Jumbotron fluid>*/}
            {/*    <Container fluid>*/}
            {/*        <h1 className="display-3">Fluid jumbotron</h1>*/}
            {/*    </Container>*/}
            {/*</Jumbotron>*/}
            {!token && <>
                <NavLink tag={Link} className="text-dark" to="/login">Login</NavLink>
                <NavLink tag={Link} className="text-dark" to="/register">Register</NavLink>
            </>}
            {token && <>
                <NavLink tag={Link} className="text-dark" to="/userprofile">Profile</NavLink>
                <NavLink tag={Link} className="text-dark" onClick={Logout } to="/logout">Logout</NavLink>
                </>
            }
        </header>
        )

};

export default NavUser;


