import React from 'react';
import { Button, Container, Menu } from 'semantic-ui-react';
import { useStore } from '../stores/store';

export default function Navbar(){

    const {activityStore} = useStore();

    return (
        <Menu inverted fixed='top'>
            <Container>
                <Menu.Item>
                    <img src="/assets/Images/logo.png" alt="logo" style={{marginRight: '10px'}}/>
                    Reactivities
                </Menu.Item>
                <Menu.Item name='Activities'>
                    <Button onClick={() => activityStore.openForm()} positive content='Create Activity' />
                </Menu.Item>
            </Container>
        </Menu>
    )
}