import React from "react";
import { Link, BrowserRouter, Switch, Route } from "react-router-dom";

import Menu from "@material-ui/core/Menu";
import MenuItem from "@material-ui/core/MenuItem";

import IconButton from "@material-ui/core/IconButton";
import AccountCircleRoundedIcon from "@material-ui/icons/AccountCircleRounded";
import Can from "../Can";

const ProfileButton = (props) => {
    const [anchorEl, setAnchorEl] = React.useState(null);
    const handleClickProfile = (event) => {
        setAnchorEl(event.currentTarget);
    };

    const handleCloseProfile = () => {
        setAnchorEl(null);
    };
    console.log(props.user.role);
    console.log('Profile btton');
    return (
        <div>
            <IconButton edge="end" onClick={handleClickProfile} color="default">
                <AccountCircleRoundedIcon />
            </IconButton>
            <Menu
                id="profile-menu"
                anchorEl={anchorEl}
                keepMounted
                open={Boolean(anchorEl)}
                onClose={handleCloseProfile}
            >
                <Link to="/profil">
                    {" "}
                    <MenuItem onClick={handleCloseProfile}>Profil</MenuItem>
                </Link>
                <Link to="/kalendar">
                    {" "}
                    <MenuItem onClick={handleCloseProfile}>Kalendar</MenuItem>
                </Link>
                <Link to="/clanarine">
                    {" "}
                    <MenuItem onClick={handleCloseProfile}>Članarine</MenuItem>
                </Link>
                {" "}
               

                <Can
                    role={props.user.role}
                    perform="lessons:visit"
                    data={{
                        userId: props.user.Id,
                    }}
                    yes={() => (
                        <Link to="/lekcije">
                            <MenuItem onClick={handleCloseProfile}>Lekcije</MenuItem>
                        </Link>
                    )}
                />
                <Can
                    role={props.user.role}
                    perform="reservations:visit"
                    data={{
                        userId: props.user.Id,
                    }}
                    yes={() => (
                        <Link to="/rezervacije">
                            {" "}
                            <MenuItem onClick={handleCloseProfile}>Rezervacije</MenuItem>
                        </Link>
                    )}
                />
                <Can
                    role={props.user.role}
                    perform="users:visit"
                    data={{
                        userId: props.user.Id,
                    }}
                    yes={() => (
                        <Link to="/korisnici">
                            {" "}
                            <MenuItem onClick={handleCloseProfile}>Korisnici</MenuItem>
                        </Link>
                    )}
                />
            </Menu>
        </div>
    );
};

export default ProfileButton;
