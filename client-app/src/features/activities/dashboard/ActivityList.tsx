import { observer } from 'mobx-react-lite';
import React, { SyntheticEvent, useState } from 'react';
import { Button, Icon, Item, Label, Segment } from 'semantic-ui-react';
import { useStore } from '../../../app/stores/store';

export default observer(function ActivityList(){
    const [target, setTarget] = useState('');
    const {activityStore} = useStore();
    const {activitiesByDate: activities, deleteActivity, loading} = activityStore;


    function handleActivityDelete(e: SyntheticEvent<HTMLButtonElement>, id: string){
        setTarget(e.currentTarget.name);
        deleteActivity(id)
    }

    return (
        <Segment>
            <Item.Group divided>
                {activities.map(activity => (
                    <Item key={activity.id}>
                        <Item.Content>
                            <Item.Header as='a'>{activity.title}</Item.Header>
                            <Item.Meta>{activity.date}</Item.Meta>
                            <Item.Description>
                                <div>{activity.description}</div>
                                <div>{activity.city}, {activity.venue}</div>
                            </Item.Description>
                            <Item.Extra>
                                <Button onClick={() => activityStore.selectActivity(activity.id)} floated='right' content='View' color='blue' />
                                <Button name={activity.id}
                                        loading={loading && target === activity.id} 
                                        onClick={(e) => handleActivityDelete(e, activity.id)} 
                                        floated='right' 
                                        content='Delete' 
                                        color='red'
                                        animated='fade'>
                                    <Button.Content visible>
                                        Remove
                                    </Button.Content>
                                    <Button.Content hidden>
                                        <Icon name='delete' />
                                    </Button.Content>
                                </Button>
                                <Label basic content={activity.category} />
                                
                               
                            </Item.Extra>
                        </Item.Content>
                    </Item>
                ))}
            </Item.Group>
        </Segment>
    )
})
