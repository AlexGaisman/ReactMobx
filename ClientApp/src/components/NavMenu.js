import React, { useState } from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink, NavbarText } from 'reactstrap';
import { Link } from 'react-router-dom';
import {NavUser} from '../auth/NavUser'
import './NavMenu.css';

export const NavMenu = (props) => {

    const [toggleNavbar, setToggleNavbar] = useState(false);

    return (
        <header>
            <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3" light>
                <Container>
                    <NavbarBrand tag={Link} to="/">Tracker</NavbarBrand>
                    <NavbarToggler onClick={() => { setToggleNavbar(!toggleNavbar) }} className="mr-2" />
                    <Collapse className="d-sm-inline-flex" isOpen={!toggleNavbar} navbar>
                        <ul className="navbar-nav flex-grow">
                            <NavItem>
                                <NavLink tag={Link} className="text-dark" to="/">Home</NavLink>
                            </NavItem>
                            <NavItem>
                                <NavLink tag={Link} className="text-dark" to="/counter">Counter</NavLink>
                            </NavItem>
                            <NavItem>
                                <NavLink tag={Link} className="text-dark" to="/fetch-data">Fetch data</NavLink>
                            </NavItem>
                        </ul>.                       
                    </Collapse>
                    <NavUser/>
                </Container>
            </Navbar>
        </header>
        )

};

export default NavMenu;


